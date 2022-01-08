namespace SolidMatrix.Affair.Api.Catalogs;

public static class Const
{
    public static string CaptionFileName = "caption.txt";
    public static string TitleFileName = "title.txt";
    public static string SubTitleFileName = "subtitle.txt";

    public static string DesignFileExt = ".jpg";

    public static string WorkdirEnvName = "WORKDIR";
    public static string DefaultWorkdir = "Workdir";
    public static string WorkdirPath => Environment.GetEnvironmentVariable(WorkdirEnvName) ?? DefaultWorkdir;

    public static Dictionary<string, ResourceStyle> ResourceStyles = new Dictionary<string, ResourceStyle>{
        {"thumbnail",new ResourceStyle( "thumbnail", "_thumbnail", 500, 500)},
        {"small",new ResourceStyle("small", "_small", 750, 750)},
        {"medium", new ResourceStyle( "medium", "_medium", 1000, 1000)},
        {"large",new ResourceStyle( "large", "_large", 2000, 2000)},
    };
}
