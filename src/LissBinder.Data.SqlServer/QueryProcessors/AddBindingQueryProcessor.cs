using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Data.SqlServer.Common;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class AddBindingQueryProcessor : IAddBindingQueryProcessor
    {
        private readonly DbContext _context;

        public AddBindingQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBindingAsync(Binding binding, int pharmacyId)
        {
            try
            {
                using (var connection = new SqlConnection(_context.ConnectionString))
                {
                    var commandText = "dbo.azure_liss_binding_create";
                    var commandType = CommandType.StoredProcedure;
                    var commandParameters = new SqlParameter[] {
                        new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, 
                            ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                         new SqlParameter("@codepst", SqlDbType.BigInt, 4),
                         new SqlParameter("@nomenid", SqlDbType.Int, 4),
                         new SqlParameter("@prepid", SqlDbType.Int, 4),
                         new SqlParameter("@drugformid", SqlDbType.Int),
                         new SqlParameter("@id_pharmacy", SqlDbType.Int, 4),
                         new SqlParameter("@descid", SqlDbType.Int, 4),
                         new SqlParameter("@prodcode", SqlDbType.Int, 4)};

                    //@codepst bigint,
                    //@nomenid int,
                    //@prepid int, 
                    //@drugformid int, 
                    //@id_pharmacy int, 
                    //@descid int, 
                    //@prodcode int

                    using (var command = SqlAccessHelper.CreateCommand(
                        connection, commandText, commandType, commandParameters))
                    {
                        command.Parameters["@codepst"].Value = binding.PharmacyDrugCode;
                        command.Parameters["@nomenid"].Value = binding.NomenId;
                        command.Parameters["@prepid"].Value = binding.PrepId;
                        command.Parameters["@drugformid"].Value = binding.DrugformId;
                        command.Parameters["@id_pharmacy"].Value = pharmacyId;
                        command.Parameters["@descid"].Value = binding.DescriptionId;
                        command.Parameters["@prodcode"].Value = binding.PharmacyDrugProdCode;

                        await connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();
                    }

                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
