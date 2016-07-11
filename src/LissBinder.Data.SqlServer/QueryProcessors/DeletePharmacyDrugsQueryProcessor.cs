using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Data.SqlServer.Common;


namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class DeletePharmacyDrugsQueryProcessor : IDeletePharmacyDrugsQueryProcessor
    {
        private readonly DbContext _context;

        public DeletePharmacyDrugsQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDrugsAsync(int pharmacyId)
        {
            try
            {
                using (var connection = new SqlConnection(_context.ConnectionString))
                {
                    var commandText = "dbo.azure_liss_druglist_delete_by_pharmacyid";
                    var commandType = CommandType.StoredProcedure;
                    var commandParameters = new SqlParameter[] {
                        new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, 
                            ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                        new SqlParameter("@id_pharmacy", SqlDbType.Int, 4)};

                
                    using (var command = SqlAccessHelper.CreateCommand(
                        connection, commandText, commandType, commandParameters))
                    {
                        command.Parameters["@id_pharmacy"].Value = pharmacyId;
                       
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
