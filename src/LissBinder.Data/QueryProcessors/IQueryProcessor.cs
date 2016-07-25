using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IQueryProcessor<TEntity>
    {
        Task<IEnumerable<TEntity>> SelectAllAsync();
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
