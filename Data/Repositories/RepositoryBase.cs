using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _db;
        public RepositoryBase(DataContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            IQueryable<T> query = _db;

            return await query.ToListAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _db;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _db.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _db;

            return await query.FirstOrDefaultAsync(expression);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
