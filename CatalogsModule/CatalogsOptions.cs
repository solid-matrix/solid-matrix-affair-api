namespace SolidMatrix.Affair.Api.CatalogsModule;

public class CatalogsOptions
{
    public string CaptionFileName { get; set; } = "caption.txt";
    public string TitleFileName { get; set; } = "title.txt";
    public string SubTitleFileName { get; set; } = "subtitle.txt";
    public string SubImageFileExtName { get; set; } = ".jpg";
    public string WorkdirPath { get; set; } = "Workdir";
    public string SubImageMIME { get; set; } = "image/jpeg";
    public int ResolveWorkdirTimeInterval { get; set; } = 5;
    public List<SubImageStyle> SubImageStyles { get; set; } = null!;
}
