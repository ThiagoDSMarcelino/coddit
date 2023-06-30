namespace Coddit.Repositories;

using Model;

public class MemberRepository : IRepository<Member>
{
    private readonly CodditContext entity;

    public MemberRepository(CodditContext context)
        => entity = context;

    public async Task Add(Member obj)
    {
        await entity.AddAsync(obj);
        await entity.SaveChangesAsync();
    }

    public async Task Delete(Member obj)
    {
        entity.Remove(obj);
        await entity.SaveChangesAsync();
    }

    public async Task Update(Member obj)
    {
        entity.Update(obj);
        await entity.SaveChangesAsync();
    }

    public async Task<Member> Get(Expression<Func<Member, bool>> exp)
        => await entity.Members.FirstOrDefaultAsync(exp);

    public async Task<bool> Exist(Expression<Func<Member, bool>> exp)
        => await entity.Members.AnyAsync(exp);

    public async Task<List<Member>> Filter(Expression<Func<Member, bool>> exp)
        => await entity.Members.Where(exp).ToListAsync();
}