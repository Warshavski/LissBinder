using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public abstract class BaseQueryProcessor<TEntity>
    {
        protected abstract TEntity CreateEntity(IDataRecord record);

        protected async Task<IEnumerable<TEntity>> SelectEntityListAsync(DbCommand command)
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                var entityList = new List<TEntity>();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var entity = CreateEntity(reader);
                        entityList.Add(entity);
                    }
                    return entityList;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
