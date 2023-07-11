namespace Coddit.Repositories;

public class VoteRepository : IRepository<Vote>
{
    private readonly CodditContext _entity;

    public VoteRepository(CodditContext context)
        => _entity = context;

    public async Task Add(Vote obj)
    {
        await _entity.AddAsync(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task Delete(Vote obj)
    {
        _entity.Remove(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task Update(Vote obj)
    {
        _entity.Update(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task<Vote?> Get(Expression<Func<Vote, bool>> exp)
        => await _entity.Votes.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Vote, bool>> exp)
        => await _entity.Votes.AnyAsync(exp);

    public async Task<List<Vote>> Filter(Expression<Func<Vote, bool>> exp)
        => await _entity.Votes.Where(exp).ToListAsync();
}