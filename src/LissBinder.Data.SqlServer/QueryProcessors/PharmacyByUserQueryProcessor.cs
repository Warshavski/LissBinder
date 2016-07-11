using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Data.SqlServer.Common;


namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class PharmacyByUserQueryProcessor : IPharmacyByUserQueryProcessor
    {
        private readonly DbContext _context;

        public PharmacyByUserQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<Pharmacy> GetPharmacyAsync(int userId)
        {
            using (var connection = new SqlConnection(_context.ConnectionString))
            {
                var commandText = "dbo.azure_liss_pharmacy_select_by_userid";
                var commandType = CommandType.StoredProcedure;
                var commandParameters = new SqlParameter[] {
                    new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                    new SqlParameter("@id_user", SqlDbType.Int, 4) };

                using (var command = SqlAccessHelper.CreateCommand(
                    connection, commandText, commandType, commandParameters))
                {
                    command.Parameters["@id_user"].Value = userId;

                    await connection.OpenAsync();

                    return await SqlAccessHelper.GetEntityAsync<Pharmacy>(
                        connection, command, GetPharmacyFromReader);
                }
            }
        }

        private Pharmacy GetPharmacyFromReader(SqlDataReader reader)
        {
            /** Data columns order : 
            *   0. id_pharmacy  - int
            *   1. name         - string
            *   2. address      - string
            *   3. worktime     - string
            *   4. reserv_time  - string
            *   5. phone_number - string
            */

            var pharmacy = new Pharmacy();

            pharmacy.Id = reader.GetFieldValue<int>(0);
            pharmacy.Name = reader.GetFieldValue<string>(1);
            pharmacy.Address = reader.GetFieldValue<string>(2);
            pharmacy.WorkTime = reader.GetFieldValue<string>(3);
            pharmacy.ReservTime = reader.GetFieldValue<int>(4);
            pharmacy.PhoneNumber = reader.GetFieldValue<string>(5);

            return pharmacy;
        }
    }
}
