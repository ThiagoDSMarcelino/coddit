using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

using Model;

public class UserRepository : IRepository<User>
{
    private CodditContext _entity;
    
    public UserRepository(CodditContext service)
        => this._entity = service;

    public async Task<List<User>> Filter(Expression<Func<User, bool>> exp)
    {
        return await _entity.Users
            .Where(exp)
            .ToListAsync();
    }

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
}