namespace SolidMatrix.Affair.Api.Catalogs;

public static class Helper
{
    public static bool IsImageFile(string path)
    {
        string fileName = Path.GetFileName(path);
        string extName = Path.GetExtension(fileName);
        switch (extName.ToLower())
        {
            case ".jpg":
            case ".jpeg":
            case ".png":
            case ".gif":
            case ".bmp":
            case ".tif":
            case ".tiff":
            case ".webp":
                return true;
            default:
                return false;
        }
    }

    public static string FileReadAllTextOrEmpty(string path)
    {
        try
        {
            return File.ReadAllText(path);
        }
        catch
        {
            return "";
        }
    }
}
