namespace Coddit.Repositories;

using Model;

public class UserRepository : IRepository<User>
{
    private readonly CodditContext entity;
    
    public UserRepository(CodditContext context)
        => entity = context;
        
    public async Task Add(User obj)
    {
        await entity.AddAsync(obj);
        await entity.SaveChangesAsync();
    }
    
    public async Task Delete(User obj)
    {
        entity.Remove(obj);
        await entity.SaveChangesAsync();
    }
    
    public async Task Update(User obj)
    {
        entity.Update(obj);
        await entity.SaveChangesAsync();
    }
    
    public async Task<User> Get(Expression<Func<User, bool>> exp)
        => await entity.Users.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<User, bool>> exp)
        => await entity.Users.AnyAsync(exp);

    public async Task<List<User>> Filter(Expression<Func<User, bool>> exp)
        => await entity.Users.Where(exp).ToListAsync();
}