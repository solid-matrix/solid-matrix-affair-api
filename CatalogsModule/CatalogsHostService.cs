using Newtonsoft.Json.Linq;
using System.Threading;

namespace SolidMatrix.Affair.Api.CatalogsModule;

public class CatalogsHostService : IHostedService
{
    private readonly CatalogsService _catalogsService;

    public CatalogsHostService(CatalogsService catalogsService, IHostApplicationLifetime appLifetime)
    {
        _catalogsService = catalogsService;
        appLifetime.ApplicationStarted.Register(() =>
        {
            _catalogsService.Initialize();
            TimedResolveWorkdir(appLifetime.ApplicationStopping);
        });
    }

    public void TimedResolveWorkdir(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _catalogsService.ResolveWorkdir();
                await Task.Delay(TimeSpan.FromSeconds(_catalogsService.Options.ResolveWorkdirTimeInterval), cancellationToken);
            }
        }, cancellationToken);

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
