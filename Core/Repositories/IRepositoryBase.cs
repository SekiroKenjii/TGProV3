using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetWithExpressionAsync(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, List<string>? includes = null);
        IQueryable<T> GetAllIQueryable();
        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
