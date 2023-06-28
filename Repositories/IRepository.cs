namespace Backend.Repositories;

public interface IRepository<T, TKey>
{
    Task<T> GetByPK(TKey key);

    Task<bool> Exist(Expression<Func<T, bool>> exp);

    Task Add(T obj);
    
    Task Delete(T obj);
    
    Task Update(T obj);

    Task<List<T>> Filter(Expression<Func<T, bool>> exp);
}