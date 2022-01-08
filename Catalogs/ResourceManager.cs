using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace SolidMatrix.Affair.Api.Catalogs;

public static class ResourceManager
{
    // global variables
    public static Meta Meta;
    public static Dictionary<string, Catalog> CatalogDict = new Dictionary<string, Catalog>();
    public static Dictionary<string, Design> DesignDict = new Dictionary<string, Design>();
    private static object Locker = new object();

    public static void InitMeta()
    {
        lock (Locker)
        {
            Console.WriteLine("\tLoad Meta Start");

            var meta = Meta.LoadFromPath(Const.WorkdirPath);
            foreach (var catalogPath in Directory.GetDirectories(Const.WorkdirPath))
            {
                if (Path.GetFileName(catalogPath).StartsWith('_')) continue;

                Catalog catalog = Catalog.LoadFromPath(catalogPath);
                foreach (var designPath in Directory.GetFiles(catalogPath))
                {
                    if (!Helper.IsImageFile(designPath)) continue;
                    Design design = Design.LoadFromPath(designPath, catalog);
                    catalog.Designs.Add(design);
                }
                catalog.Designs.Sort();
                meta.Catalogs.Add(catalog);
            }

            meta.Catalogs.Sort();

            Dictionary<string, Catalog> catalogDict = new Dictionary<string, Catalog>();
            Dictionary<string, Design> designDict = new Dictionary<string, Design>();

            meta.Catalogs.ForEach(catalog =>
            {
                catalogDict[catalog.Id] = catalog;
                catalog.Designs.ForEach(design =>
                {
                    designDict[design.Id] = design;
                });
            });

            Meta = meta;
            CatalogDict = catalogDict;
            DesignDict = designDict;
            Console.WriteLine("\tLoad Meta End");
        }

    }

    // init
    public static void Init()
    {
        Directory.CreateDirectory(Const.WorkdirPath);
        InitMeta();
    }

    // methods 
    public static Design? GetDesign(string id)
    {
        try
        {
            var design = DesignDict[id];
            return design;
        }
        catch
        {
            return null;
        }
    }

    public static Catalog? GetCatalog(string id)
    {
        try
        {
            var catalog = CatalogDict[id];
            return catalog;
        }
        catch
        {
            return null;
        }
    }

    public static string? GetStyledResourcePath(Design design, string styleName)
    {
        ResourceStyle? style = Const.ResourceStyles.ContainsKey(styleName) ? Const.ResourceStyles[styleName] : null;
        if (style == null) return null;
        var filepath = Path.Combine(Const.WorkdirPath, style.SubPath, design.Catalog.Name, design.Fullname);
        if (!File.Exists(filepath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            using (var image = Image.Load(design.OriginalPath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(style.MaxWidth, style.MaxHeight) }));
                image.SaveAsJpeg(filepath, new JpegEncoder { Quality = 75, Subsample = JpegSubsample.Ratio444 });
            }
        }
        return Path.GetFullPath(filepath);
    }
}