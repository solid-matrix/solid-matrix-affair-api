namespace SolidMatrix.Affair.Api.Catalogs;

public class Meta
{
    public string Caption;
    public string Title;
    public string SubTitle;
    public List<Catalog> Catalogs = new List<Catalog>();
    public Dictionary<string, ResourceStyle> Styles = Const.ResourceStyles;

    internal string OriginalPath;

    public static Meta LoadFromPath(string path)
    {
        var meta = new Meta();
        meta.OriginalPath = Path.GetFullPath(path);

        meta.Caption = Helper.FileReadAllTextOrEmpty(Path.Combine(meta.OriginalPath, Const.CaptionFileName));
        meta.Title = Helper.FileReadAllTextOrEmpty(Path.Combine(meta.OriginalPath, Const.TitleFileName));
        meta.SubTitle = Helper.FileReadAllTextOrEmpty(Path.Combine(meta.OriginalPath, Const.SubTitleFileName));
        return meta;
    }
}
