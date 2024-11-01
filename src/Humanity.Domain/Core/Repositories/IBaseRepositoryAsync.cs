using Humanity.Domain.Core.Models;
using Humanity.Domain.Core.Specifications;

namespace Humanity.Domain.Core.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(int id);

        Task<T> GetBy(ISpecification<T> spec);
        Task<IList<T>> ListAllAsync();
        Task<IList<T>> ListAsync(ISpecification<T> spec);
        Task<T?> FirstOrDefaultAsync(ISpecification<T?> spec);
        Task<T> AddAsync(T entity);
        //Task AddAsync(T entity);

        Task<IEnumerable<T>> AddRandeAsync(IEnumerable<T> entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> CountAsync(ISpecification<T> spec);

        Task<IList<T>> RawSql(string sql);
       
    }
}
