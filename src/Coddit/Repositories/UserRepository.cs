namespace Coddit.Repositories;

using Model;

public class UserRepository : IRepository<User>
{
    private readonly CodditContext _entity;
    
    public UserRepository(CodditContext context)
        => _entity = context;
        
    public async Task Add(User obj)
    {
        await _entity.AddAsync(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task Delete(User obj)
    {
        _entity.Remove(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task Update(User obj)
    {
        _entity.Update(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task<User?> Get(Expression<Func<User, bool>> exp)
        => await _entity.Users.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<User, bool>> exp)
        => await _entity.Users.AnyAsync(exp);

    public async Task<List<User>> Filter(Expression<Func<User, bool>> exp)
        => await _entity.Users.Where(exp).ToListAsync();
}