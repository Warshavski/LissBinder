using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.Extensions;
using Escyug.LissBinder.Data.QueryProcessors;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class BindingsQueryProcessor : BaseQueryProcessor<Binding>, IBindingsQueryProcessor
    {
        private readonly DbContext _context;

        public BindingsQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Binding>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Binding entity)
        {
            var commandText = "dbo.azure_liss_binding_create";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("codepst", entity.PharmacyDrugCode);
                    command.AddParameter("nomenid", entity.NomenId);
                    command.AddParameter("prepid", entity.PrepId);
                    command.AddParameter("drugformid", entity.DrugformId);
                    command.AddParameter("id_pharmacy", entity.PharmacyId);
                    command.AddParameter("descid", entity.DescriptionId);
                    command.AddParameter("prodcode",entity.PharmacyDrugProdCode);

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();
                }

            }
        }

        public Task UpdateAsync(Binding entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Binding entity)
        {
            throw new NotImplementedException();
        }

        protected override Binding CreateEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
