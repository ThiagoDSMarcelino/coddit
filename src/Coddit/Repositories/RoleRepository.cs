namespace Coddit.Repositories;

using Model;

public class RoleRepository : IRepository<Role>
{
    private readonly CodditContext _entity;

    public RoleRepository(CodditContext context)
        => _entity = context;

    public async Task Add(Role obj)
    {
        await _entity.AddAsync(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task Delete(Role obj)
    {
        _entity.Remove(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task Update(Role obj)
    {
        _entity.Update(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task<Role> Get(Expression<Func<Role, bool>> exp)
        => await _entity.Roles.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Role, bool>> exp)
        => await _entity.Roles.AnyAsync(exp);

    public async Task<List<Role>> Filter(Expression<Func<Role, bool>> exp)
        => await _entity.Roles.Where(exp).ToListAsync();
}