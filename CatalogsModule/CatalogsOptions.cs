namespace SolidMatrix.Affair.Api.CatalogsModule;

public class CatalogsOptions
{
    public string CaptionFileName { get; set; } = null!;
    public string TitleFileName { get; set; } = null!;
    public string SubTitleFileName { get; set; } = null!;
    public string SubImageFileExtName { get; set; } = null!;

    public string SubImageMIME { get; set; } = null!;
    public int ResolveWorkdirTimeInterval { get; set; } = 5;
    public string DefaultPublicWorkdirPath { get; set; } = null!;
    public string DefaultPrivateWorkdirPath { get; set; } = null!;
    public List<SubImageStyle> SubImageStyles { get; set; } = null!;

    public string PublicWorkdirPath { get; set; } = null!;
    public string PrivateWorkdirPath { get; set; } = null!;
}
