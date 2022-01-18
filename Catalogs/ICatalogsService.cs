namespace SolidMatrix.Affair.Api.Catalogs;

public interface ICatalogsService : IHostedService
{
    void LoadMetadata();
    Meta GetMeta();
    Design? GetDesign(string id);
    Catalog? GetCatalog(string id);
}