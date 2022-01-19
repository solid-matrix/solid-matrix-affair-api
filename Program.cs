using SolidMatrix.Affair.Api.CatalogsModule;
using SolidMatrix.Affair.Api.UserModule;
using SolidMatrix.Affair.Api.WarehouseModule;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCatalogsModule()
    //.AddUserModule()
    //.AddWarehouseModule()
    .AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("*")))
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.IncludeFields = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    })
;

var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
