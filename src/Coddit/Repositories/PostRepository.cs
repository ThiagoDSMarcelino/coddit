namespace Coddit.Repositories;

using Model;

public class PostRepository : IRepository<Post>
{
    private readonly CodditContext _entity;
    
    public PostRepository(CodditContext context)
        => _entity = context;
        
    public async Task Add(Post obj)
    {
        await _entity.AddAsync(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task Delete(Post obj)
    {
        _entity.Remove(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task Update(Post obj)
    {
        _entity.Update(obj);
        await _entity.SaveChangesAsync();
    }
    
    public async Task<Post?> Get(Expression<Func<Post, bool>> exp)
        => await _entity.Posts.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Post, bool>> exp)
        => await _entity.Posts.AnyAsync(exp);

    public async Task<List<Post>> Filter(Expression<Func<Post, bool>> exp)
        => await _entity.Posts.Where(exp).ToListAsync();
}