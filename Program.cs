SolidMatrix.Affair.Api.Catalogs.ResourceManager.Init();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*");
        });
});

builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
