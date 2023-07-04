namespace Coddit.Repositories.MemberReposiory;

public interface IMemberRepository : IRepository<Member>
{
    Task<List<Member>> FilterWithForums(Expression<Func<Member, bool>> exp);
}