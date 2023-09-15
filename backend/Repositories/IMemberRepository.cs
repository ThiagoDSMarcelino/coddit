namespace Coddit.Repositories;

public interface IMemberRepository : IRepository<Member>
{
    Task<List<Member>> FilterWithForums(Expression<Func<Member, bool>> exp);
    Task<List<Member>> FilterWithForumsAndPost(Expression<Func<Member, bool>> exp);
}