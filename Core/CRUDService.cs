using Microsoft.EntityFrameworkCore;
namespace SolidMatrix.Affair.Api.Core;

public class CRUDService<TDbContext> where TDbContext : DbContext
{
    private readonly TDbContext _db;

    public CRUDService(TDbContext db) => _db = db;

    public Task<List<TEntity>> IndexAsync<TEntity>() where TEntity : BasicModel
    {
        return Task.FromResult(_db.Set<TEntity>().ToList());
    }

    public Task<TEntity> ReadAsync<TEntity>(Guid id) where TEntity : BasicModel
    {
        var item = _db.Set<TEntity>().Single(i => i.Id == id);
        if (item == null) throw new ResponseException(ExceptionCode.NotFound, "Not found error");
        return Task.FromResult(item);
    }

    public async Task<TEntity> CreateAsync<TEntity>(TEntity item) where TEntity : BasicModel
    {
        _db.Set<TEntity>().Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task<TEntity> UpdateAsync<TEntity>(Guid id, TEntity item) where TEntity : BasicModel
    {
        try
        {
            item.Id = id;
            _db.Set<TEntity>().Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
        catch
        {
            throw new ResponseException(ExceptionCode.NotFound, "Not found error");
        }

    }

    public async Task<TEntity> UpdateOrCreateAsync<TEntity>(TEntity item) where TEntity : BasicModel
    {
        var exist = _db.Set<TEntity>().AsNoTracking().Any(x => x.Id == item.Id);
        if (exist)
            _db.Set<TEntity>().Update(item);
        else
            _db.Set<TEntity>().Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task DeleteAsync<TEntity>(Guid id) where TEntity : BasicModel
    {
        try
        {
            var item = _db.Set<TEntity>().Single(c => c.Id == id);
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw new ResponseException(ExceptionCode.NotFound, "Not found error");
        }
    }
}
