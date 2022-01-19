using System.Text.Json.Serialization;

namespace SolidMatrix.Affair.Api.CatalogsModule;

public class Design : IComparable
{
    public string Id => $"{CatalogId}-{Name}-{TimeStamp.Ticks}";

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public string OriginalPath { get; set; } = null!;

    [JsonIgnore]
    public DateTime TimeStamp { get; set; }

    [JsonIgnore]
    public Catalog Catalog { get; set; } = null!;

    public string CatalogId => Catalog.Id;

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        var other = (Design)obj;
        return -string.Compare(Name, other.Name);
    }
}