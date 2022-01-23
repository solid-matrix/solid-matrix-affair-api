using Microsoft.AspNetCore.Mvc;
using SolidMatrix.Affair.Api.CatalogsModule;
using SolidMatrix.Affair.Api.Core;

namespace SolidMatrix.Affair.Api.Controllers;

[Route("[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly ILogger<CatalogsController> _logger;
    private readonly CatalogsService _catalogsService;

    public CatalogsController(ILogger<CatalogsController> logger, CatalogsService catalogsService)
    {
        _logger = logger;
        _catalogsService = catalogsService;
    }

    [HttpGet]
    public IActionResult Home()
    {
        return new JsonResult(UniformResult.Ok(null));
    }

    [HttpGet("meta")]
    public IActionResult GetMeta()
    {
        return new JsonResult(UniformResult.Ok(_catalogsService.Metadata));
    }

    [HttpGet("init")]
    public IActionResult InitMeta()
    {
        _catalogsService.ResolveWorkdir();
        return new JsonResult(UniformResult.Ok(null));
    }

    [HttpGet("catalog/{id}")]
    public IActionResult GetCatalog(string id)
    {
        var data = _catalogsService.GetCatalogById(id);
        var msg = data is null ? UniformResult.Error($"Catalog {id} Not Found") : UniformResult.Ok(data);
        return new JsonResult(msg);
    }

    [HttpGet("design/{id}")]
    public IActionResult GetDesign(string id)
    {
        var data = _catalogsService.GetDesignById(id);
        var msg = data is null ? UniformResult.Error($"Design {id} Not Found") : UniformResult.Ok(data);
        return new JsonResult(msg);
    }

    [HttpGet("subimage/{id}/{style}")]
    public IActionResult GetStyledResource(string id, string style)
    {
        var path = _catalogsService.GetOrCreateSubImagePath(id, style);
        if (path is null)
            return new JsonResult(UniformResult.Error($"SubImage {id}:{style} Not Found"));
        else
            return new PhysicalFileResult(path, _catalogsService.Options.SubImageMIME);

    }
}