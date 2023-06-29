namespace Coddit.Repositories;

public interface IRepository<T>
{
    Task Add(T obj);
    
    Task Delete(T obj);
    
    Task Update(T obj);

    Task<T> Get(Expression<Func<T, bool>> exp);

    Task<bool> Exist(Expression<Func<T, bool>> exp);

    Task<List<T>> Filter(Expression<Func<T, bool>> exp);
}