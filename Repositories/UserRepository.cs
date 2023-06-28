namespace Backend.Repositories;

using Model;

public class UserRepository : IRepository<User, long>
{
    private CodditContext _entity;
    
    public UserRepository(CodditContext service)
        => _entity = service;

    public async Task<User> GetByPK(long key)
        => await _entity.Users
            .FirstOrDefaultAsync(user => user.Id == key);

    public async Task<bool> Exist(Expression<Func<User, bool>> exp)
        => await _entity.Users
            .AnyAsync(exp);
    
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
    
    public async Task<List<User>> Filter(Expression<Func<User, bool>> exp)
        => await _entity.Users
            .Where(exp).ToListAsync();
}