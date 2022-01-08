namespace SolidMatrix.Affair.Api.Catalogs;

public class ResourceStyle
{
    public string SubUrl;
    internal string SubPath;
    public int MaxHeight;
    public int MaxWidth;

    public ResourceStyle(string subUrl, string subPath, int maxHeight, int maxWidth)
    {
        SubUrl = subUrl;
        SubPath = subPath;
        MaxHeight = maxHeight;
        MaxWidth = maxWidth;
    }
}