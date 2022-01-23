using Microsoft.AspNetCore.Mvc;
using SolidMatrix.Affair.Api.Core;
using SolidMatrix.Affair.Api.WarehouseModule;

namespace SolidMatrix.Affair.Api.Controllers;

[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger;
    private readonly CRUDService<DbContextService> _crudService;

    public WarehouseController(ILogger<WarehouseController> logger, CRUDService<DbContextService> crudService)
    {
        _logger = logger;
        _crudService = crudService;
    }

    [HttpGet]
    public IActionResult Home()
    {
        return new JsonResult(UniformResult.Ok(null));
    }

    // Category Actions
    [HttpGet("category")]
    public async Task<IActionResult> CategoryIndex()
    {
        var categories = await _crudService.IndexAsync<Category>();
        return new JsonResult(UniformResult.Ok(categories));
    }

    [HttpGet("category/{id}")]
    public async Task<IActionResult> CategoryRead(Guid id)
    {
        var category = await _crudService.ReadAsync<Category>(id);
        return new JsonResult(UniformResult.Ok(category));
    }

    [HttpPost("category")]
    public async Task<IActionResult> CategoryCreate([FromBody] Category category)
    {
        if (!ModelState.IsValid) throw new ResponseException(ModelState);

        var c = await _crudService.CreateAsync<Category>(category);
        return new JsonResult(UniformResult.Ok(c));
    }

    [HttpPut("category/{id}")]
    public async Task<IActionResult> CategoryUpdate(Guid id, [FromBody] Category category)
    {
        if (!ModelState.IsValid) throw new ResponseException(ModelState);

        var c = await _crudService.UpdateAsync<Category>(id, category);
        return new JsonResult(UniformResult.Ok(c));
    }

    [HttpPut("category")]
    public async Task<IActionResult> CategoryUpdateOrCreate([FromBody] Category category)
    {
        if (!ModelState.IsValid) throw new ResponseException(ModelState);

        var c = await _crudService.UpdateOrCreateAsync<Category>(category);
        return new JsonResult(UniformResult.Ok(c));
    }

    [HttpDelete("category/{id}")]
    public async Task<IActionResult> CategoryDelete(Guid id)
    {
        await _crudService.DeleteAsync<Category>(id);
        return new JsonResult(UniformResult.Ok(null));
    }

    // StoreItem Actions
    [HttpPost("instore")]
    public async Task<IActionResult> InStore([FromBody] StoreItem storeItem)
    {
        if (!ModelState.IsValid) throw new ResponseException(ModelState);
        storeItem.Status = ItemStatus.InStore;

        await _crudService.UpdateOrCreateAsync<StoreItem>(storeItem);
        return new JsonResult(UniformResult.Ok(null));
    }

    [HttpPost("outstore")]
    public async Task<IActionResult> OutStore([FromBody] StoreItem storeItem)
    {
        if (!ModelState.IsValid) throw new ResponseException(ModelState);
        storeItem.Status = ItemStatus.OutStore;

        await _crudService.UpdateOrCreateAsync<StoreItem>(storeItem);
        return new JsonResult(UniformResult.Ok(null));
    }

}