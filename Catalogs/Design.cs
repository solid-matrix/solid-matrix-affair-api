namespace SolidMatrix.Affair.Api.Catalogs;

public class Design : IComparable
{
    // public properties
    public string Id;
    public string Name;
    public string Fullname;
    public string CatalogId => Catalog.Id;

    // internal properties
    internal Catalog Catalog;
    internal string OriginalPath;

    // compare
    public int CompareTo(object obj)
    {
        var other = (Design)obj;
        return -string.Compare(this.Name, other.Name);
    }

    // builder
    public static Design LoadFromPath(string path, Catalog catalog)
    {
        var design = new Design();
        design.Catalog = catalog;
        design.OriginalPath = Path.GetFullPath(path);
        design.Name = Path.GetFileNameWithoutExtension(design.OriginalPath).Replace(' ', '_');
        design.Id = catalog.Id + "-" + design.Name;
        design.Fullname = design.Name + Const.DesignFileExt;

        return design;
    }

}


