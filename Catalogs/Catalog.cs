namespace SolidMatrix.Affair.Api.Catalogs;

public class Catalog : IComparable
{
    // public properties
    public string Id;
    public string Name;
    public string Title;
    public string SubTitle;
    public string Caption;
    public List<Design> Designs = new List<Design>();

    // internal properties
    internal string OriginalPath;

    // compare
    public int CompareTo(object obj)
    {
        var other = (Catalog)obj;
        return -string.Compare(this.Name, other.Name);
    }

    // builder
    public static Catalog LoadFromPath(string path)
    {
        var catalog = new Catalog();
        catalog.OriginalPath = Path.GetFullPath(path);

        catalog.Name = Path.GetFileName(catalog.OriginalPath).Replace(' ', '_');
        catalog.Id = catalog.Name;
        catalog.Caption = Helper.FileReadAllTextOrEmpty(Path.Combine(catalog.OriginalPath, Const.CaptionFileName));
        catalog.Title = Helper.FileReadAllTextOrEmpty(Path.Combine(catalog.OriginalPath, Const.TitleFileName));
        catalog.SubTitle = Helper.FileReadAllTextOrEmpty(Path.Combine(catalog.OriginalPath, Const.SubTitleFileName));

        return catalog;
    }
}
