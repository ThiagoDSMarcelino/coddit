namespace Coddit.Repositories;

using Model;
using System.Linq;

public class ForumRepository : IForumRepository
{
    private readonly CodditContext _entity;
    
    public ForumRepository(CodditContext context)
        => _entity = context;
        
    public async Task Add(Forum obj)
    {
        await _entity.AddAsync(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task Delete(Forum obj)
    {
        _entity.Remove(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task Update(Forum obj)
    {
        _entity.Update(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task<Forum?> Get(Expression<Func<Forum, bool>> exp)
        => await _entity.Forums.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Forum, bool>> exp)
        => await _entity.Forums.AnyAsync(exp);

    public async Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
        => await _entity.Forums.Where(exp).ToListAsync();

    public async Task<List<Forum>> FilterWithMembers(Expression<Func<Forum, bool>> exp)
        => await _entity.Forums.Where(exp).Include(f => f.Members).ToListAsync();

    public async Task<List<Forum>> FilterWithPost(Expression<Func<Forum, bool>> exp)
        => await _entity.Forums.Where(exp).Include(f => f.Posts).ToListAsync();
}