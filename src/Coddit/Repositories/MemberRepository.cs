namespace Coddit.Repositories;

using Model;

public class MemberRepository : IMemberRepository
{
    private readonly CodditContext _entity;

    public MemberRepository(CodditContext context)
        => _entity = context;

    public async Task Add(Member obj)
    {
        await _entity.AddAsync(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task Delete(Member obj)
    {
        _entity.Remove(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task Update(Member obj)
    {
        _entity.Update(obj);
        await _entity.SaveChangesAsync();
    }

    public async Task<Member> Get(Expression<Func<Member, bool>> exp)
        => await _entity.Members.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Member, bool>> exp)
        => await _entity.Members.AnyAsync(exp);

    public async Task<List<Member>> Filter(Expression<Func<Member, bool>> exp)
        => await _entity.Members.Where(exp).ToListAsync();

    public async Task<List<Member>> FilterWithForums(Expression<Func<Member, bool>> exp)
        => await _entity.Members.Where(exp).Include(m => m.Forum).ToListAsync();
}