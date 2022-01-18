namespace SolidMatrix.Affair.Api.Catalogs;

public class CatalogsService : IHostedService
{
    private readonly ILogger _logger;

    public CatalogsService(ILogger<CatalogsService> logger, IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        appLifetime.ApplicationStarted.Register(OnStarted);
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _logger.LogInformation("2. OnStarted has been called.");
    }

}
