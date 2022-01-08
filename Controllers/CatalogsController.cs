using Microsoft.AspNetCore.Mvc;
using SolidMatrix.Affair.Api.Catalogs;

namespace SolidMatrix.Affair.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogsController : ControllerBase
{
    [HttpGet("meta")]
    public IActionResult GetMeta()
    {
        var data = ResourceManager.Meta;
        UniformMessage msg = new SuccessMessage(data);
        return new JsonResult(msg);
    }

    [HttpGet("init")]
    public IActionResult InitMeta()
    {
        ResourceManager.InitMeta();
        UniformMessage msg = new SuccessMessage(null);
        return new JsonResult(msg);
    }


    [HttpGet("catalog/{id}")]
    public IActionResult GetCatalog(string id)
    {
        var data = ResourceManager.GetCatalog(id);
        UniformMessage msg = data == null ? new ErrorMessage(null, "not found" + id) : new SuccessMessage(data);
        return new JsonResult(msg);
    }

    [HttpGet("design/{id}")]
    public IActionResult GetDesign(string id)
    {
        var data = ResourceManager.GetDesign(id);
        UniformMessage msg = data == null ? new ErrorMessage(null, "not found" + id) : new SuccessMessage(data);
        return new JsonResult(msg);
    }

    [HttpGet("res/{id}/{style}")]
    public IActionResult GetStyledResource(string id, string style)
    {
        var design = ResourceManager.GetDesign(id);
        if (design == null) return new NotFoundResult();
        var path = ResourceManager.GetStyledResourcePath(design, style);
        if (path == null) return new NotFoundResult();
        return new PhysicalFileResult(path, "image/jpeg");
    }
}