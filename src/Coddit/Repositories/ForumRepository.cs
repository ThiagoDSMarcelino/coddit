namespace Coddit.Repositories;

using Model;

public class ForumRepository : IRepository<Forum>
{
    private readonly CodditContext entity;
    
    public ForumRepository(CodditContext context)
        => entity = context;
        
    public async Task Add(Forum obj)
    {
        await entity.AddAsync(obj);
        await entity.SaveChangesAsync();
    }
    
    public async Task Delete(Forum obj)
    {
        entity.Remove(obj);
        await entity.SaveChangesAsync();
    }
    
    public async Task Update(Forum obj)
    {
        entity.Update(obj);
        await entity.SaveChangesAsync();
    }
    
    public async Task<Forum> Get(Expression<Func<Forum, bool>> exp)
        => await entity.Forums.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Forum, bool>> exp)
        => await entity.Forums.AnyAsync(exp);

    public async Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
        => await entity.Forums.Where(exp).ToListAsync();
}