namespace Coddit.Repositories;

public interface IForumRepository : IRepository<Forum>
{
    Task<List<Forum>> FilterWithMembers(Expression<Func<Forum, bool>> exp);

    Task<Forum?> GetWithPost(Expression<Func<Forum, bool>> exp);
}