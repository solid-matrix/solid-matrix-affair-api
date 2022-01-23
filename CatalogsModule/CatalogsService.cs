using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

using SolidMatrix.Affair.Api.Core;
using System.Threading;

namespace SolidMatrix.Affair.Api.CatalogsModule;

public class CatalogsService
{
    private readonly ILogger<CatalogsService> _logger;

    private readonly IConfiguration _config;

    private readonly CancellationToken _cancellationToken;

    public CatalogsOptions Options { get; set; } = null!;

    public Metadata Metadata { get; set; } = null!;

    public Dictionary<string, Catalog> CatalogDict { get; set; } = null!;

    public Dictionary<string, Design> DesignDict { get; set; } = null!;

    public Dictionary<string, SubImageStyle> SubImageStyleDict { get; set; } = null!;

    public CatalogsService(ILogger<CatalogsService> logger, IConfiguration config, IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _config = config;
        _cancellationToken = appLifetime.ApplicationStopping;
    }

    public void Initialize()
    {

        // load options
        Options = _config.GetSection("CatalogsModule").Get<CatalogsOptions>();
        Options.PublicWorkdirPath = _config.GetValue<string>("CATALOGS_PUBLIC_WORKDIR_PATH") ?? Options.DefaultPublicWorkdirPath;
        Options.PrivateWorkdirPath = _config.GetValue<string>("CATALOGS_PRIVATE_WORKDIR_PATH") ?? Options.DefaultPrivateWorkdirPath;

        // inform options
        _logger.LogInformation(
            $"Catalogs Module Initialize { Environment.NewLine}" +
            $"Public Workdir Path: {Options.PublicWorkdirPath}{Environment.NewLine}" +
            $"Private Workdir Path: {Options.PrivateWorkdirPath}{Environment.NewLine}" +
            $"SubImageStyles:{Environment.NewLine}" +
            string.Join(Environment.NewLine, Options.SubImageStyles.Select(style => $"Name: {style.Name}; Path: {style.Path}; Size: {style.MaxWidth} * {style.MaxHeight}"))
            );

        // resolve workdir one time
        ResolveWorkdir();

        // timed resolve workdir
        Task.Run(async () =>
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(Options.ResolveWorkdirTimeInterval), _cancellationToken);
                ResolveWorkdir();
            }
        }, _cancellationToken);
    }

    public void ResolveWorkdir()
    {
        _logger.LogInformation("Resolving Workdir");

        Metadata = LoadMetadata(Options);
        CatalogDict = ResolveCatalogDict(Metadata, Options);
        DesignDict = ResolveDesignDict(Metadata, Options);
        SubImageStyleDict = ResolveSubImageStyleDict(Metadata, Options);
    }

    public string? GetOrCreateSubImagePath(string designId, string styleName)
    {
        var design = GetDesignById(designId);
        var style = GetSubImageStyle(styleName);
        if (design == null || style == null) return null;
        var path = Path.Combine(Path.GetFullPath(Options.PrivateWorkdirPath), style.Path, design.Catalog.Id, design.Id + Options.SubImageFileExtName);

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using var image = Image.Load(design.OriginalPath);
            image.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(style.MaxWidth, style.MaxHeight) }));
            image.SaveAsJpeg(path, new JpegEncoder { Quality = 75, Subsample = JpegSubsample.Ratio444 });
        }

        return path;
    }

    public SubImageStyle? GetSubImageStyle(string name)
    {
        if (SubImageStyleDict.TryGetValue(name, out var style))
        {
            return style;
        }
        else
        {
            return null;
        }
    }

    public Catalog? GetCatalogById(string id)
    {
        if (CatalogDict.TryGetValue(id, out var catalog))
        {
            return catalog;
        }
        else
        {
            return null;
        }
    }

    public Design? GetDesignById(string id)
    {
        if (DesignDict.TryGetValue(id, out var design))
        {
            return design;
        }
        else
        {
            return null;
        }
    }

    // static methods

    private static bool IsImageFile(string path)
    {
        string fileName = Path.GetFileName(path);
        string extName = Path.GetExtension(fileName);
        HashSet<string> imageExtNames = new() { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff", ".webp" };
        return imageExtNames.Contains(extName.ToLower());
    }

    private static string ReadTextOrEmpty(string path)
    {

        try
        {
            if (File.Exists(path))
                return File.ReadAllText(path);
            else return "";
        }
        catch
        {
            return "";
        }
    }

    private static Design LoadDesign(string path, Catalog catalog, CatalogsOptions options)
    {
        var design = new Design
        {
            OriginalPath = path,
            Name = Path.GetFileNameWithoutExtension(path).Replace(' ', '_'),
            TimeStamp = File.GetLastWriteTimeUtc(path),
            Catalog = catalog
        };

        return design;
    }

    private static Catalog LoadCatalog(string path, CatalogsOptions options)
    {
        var catalog = new Catalog
        {
            OriginalPath = Path.GetFullPath(path),
            Name = Path.GetFileName(path).Replace(' ', '_'),
            Caption = ReadTextOrEmpty(Path.Combine(path, options.CaptionFileName)),
            Title = ReadTextOrEmpty(Path.Combine(path, options.TitleFileName)),
            SubTitle = ReadTextOrEmpty(Path.Combine(path, options.SubTitleFileName))
        };

        return catalog;
    }

    private static readonly object LoadMetadataLocker = new();
    private static Metadata LoadMetadata(CatalogsOptions options)
    {
        lock (LoadMetadataLocker)
        {
            Directory.CreateDirectory(options.PublicWorkdirPath);

            var meta = new Metadata
            {
                OriginalPath = Path.GetFullPath(options.PublicWorkdirPath),
                Caption = ReadTextOrEmpty(Path.Combine(options.PublicWorkdirPath, options.CaptionFileName)),
                Title = ReadTextOrEmpty(Path.Combine(options.PublicWorkdirPath, options.TitleFileName)),
                SubTitle = ReadTextOrEmpty(Path.Combine(options.PublicWorkdirPath, options.SubTitleFileName))
            };

            foreach (var catalogPath in Directory.GetDirectories(options.PublicWorkdirPath))
            {
                if (Path.GetFileName(catalogPath).StartsWith('_')) continue;

                Catalog catalog = LoadCatalog(catalogPath, options);
                foreach (var designPath in Directory.GetFiles(catalogPath))
                {
                    if (!IsImageFile(designPath)) continue;
                    Design design = LoadDesign(designPath, catalog, options);
                    catalog.Designs.Add(design);
                }
                catalog.Designs.Sort();
                meta.Catalogs.Add(catalog);
            }
            meta.Catalogs.Sort();
            return meta;
        }
    }

    private static Dictionary<string, Catalog> ResolveCatalogDict(Metadata metadata, CatalogsOptions options)
    {
        Dictionary<string, Catalog> catalogDict = new();
        metadata.Catalogs.ForEach(c => catalogDict[c.Id] = c);
        return catalogDict;
    }

    private static Dictionary<string, Design> ResolveDesignDict(Metadata metadata, CatalogsOptions options)
    {
        Dictionary<string, Design> designDict = new();
        metadata.Catalogs.ForEach(c => c.Designs.ForEach(d => designDict[d.Id] = d));
        return designDict;
    }

    private static Dictionary<string, SubImageStyle> ResolveSubImageStyleDict(Metadata metadata, CatalogsOptions options)
    {
        Dictionary<string, SubImageStyle> subImageStyleDict = new();
        options.SubImageStyles.ForEach(style => subImageStyleDict[style.Name] = style);
        return subImageStyleDict;
    }

}
