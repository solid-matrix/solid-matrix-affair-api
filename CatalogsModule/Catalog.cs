using System.Text.Json.Serialization;

namespace SolidMatrix.Affair.Api.CatalogsModule;

public class Catalog : IComparable
{
    public string Id { get => Name; }

    public string Name { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string SubTitle { get; set; } = null!;

    public string Caption { get; set; } = null!;

    [JsonIgnore]
    public string OriginalPath { get; set; } = null!;

    public List<Design> Designs { get; set; } = new();

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        var other = (Catalog)obj;
        return -string.Compare(Name, other.Name);
    }
}