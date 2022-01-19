using System.Text.Json.Serialization;

namespace SolidMatrix.Affair.Api.CatalogsModule;

public class Metadata
{
    public string Title { get; set; } = null!;

    public string SubTitle { get; set; } = null!;

    public string Caption { get; set; } = null!;

    [JsonIgnore]
    public string OriginalPath { get; set; } = null!;

    public List<Catalog> Catalogs { get; set; } = new();
}