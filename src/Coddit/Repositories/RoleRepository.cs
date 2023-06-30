namespace Coddit.Repositories;

using Model;

public class RoleRepository : IRepository<Role>
{
    private readonly CodditContext entity;

    public RoleRepository(CodditContext context)
        => entity = context;

    public async Task Add(Role obj)
    {
        await entity.AddAsync(obj);
        await entity.SaveChangesAsync();
    }

    public async Task Delete(Role obj)
    {
        entity.Remove(obj);
        await entity.SaveChangesAsync();
    }

    public async Task Update(Role obj)
    {
        entity.Update(obj);
        await entity.SaveChangesAsync();
    }

    public async Task<Role> Get(Expression<Func<Role, bool>> exp)
        => await entity.Roles.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Role, bool>> exp)
        => await entity.Roles.AnyAsync(exp);

    public async Task<List<Role>> Filter(Expression<Func<Role, bool>> exp)
        => await entity.Roles.Where(exp).ToListAsync();
}