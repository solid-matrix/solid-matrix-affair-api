using EntityFramework.Exceptions.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SolidMatrix.Affair.Api.WarehouseModule;

public class DbContextService : DbContext
{
    private readonly IConfiguration _config;
    private readonly string _sqlitePath;

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<StoreItem> StoreItems { get; set; } = null!;

    public DbContextService(DbContextOptions<DbContextService> options, IConfiguration config) : base(options)
    {
        _config = config;

        var sqliteFileName = _config.GetSection("WarehouseModule").GetValue<string>("SqliteFileName");
        var defaultWorkdirPath = _config.GetSection("WarehouseModule").GetValue<string>("DefaultWorkdirPath");
        var workdir = _config.GetValue<string>("WAREHOUSE_WORKDIR_PATH") ?? defaultWorkdirPath;

        Directory.CreateDirectory(workdir);
        _sqlitePath = Path.Combine(workdir, sqliteFileName);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_sqlitePath}");
        optionsBuilder.UseExceptionProcessor();
    }

}