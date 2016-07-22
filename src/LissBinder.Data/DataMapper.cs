using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data
{
    public abstract class DataMapper<TEntity>
    {
        public abstract Task<IEnumerable<TEntity>> SelectAllAsync();
        public abstract Task InsertAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entity);
        public abstract Task DeleteAsync(TEntity entity);
        
        protected async Task<IEnumerable<TEntity>> SelectEntityList(DbCommand command)
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

        protected abstract TEntity CreateEntity(IDataRecord record);
    }
}
