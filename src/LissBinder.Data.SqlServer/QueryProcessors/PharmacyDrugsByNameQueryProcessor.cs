using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Data.SqlServer.Common;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class PharmacyDrugsByNameQueryProcessor : IPharmacyDrugsByNameQueryProcessor
    {
        private readonly DbContext _context;

        public PharmacyDrugsByNameQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PharmacyDrug>> GetDrugsAsync(string drugName, int pharmacyId)
        {
            //
            using (var connection = new SqlConnection(_context.ConnectionString))
            {
                var commandText = "dbo.azure_liss_druglist_select_by_name_and_pharmacyid";
                var commandType = CommandType.StoredProcedure;
                var commandParameters = new SqlParameter[] {
                    new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, 
                        ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                    new SqlParameter("@name", SqlDbType.NVarChar, 450),
                    new SqlParameter("@pharmacyId", SqlDbType.Int, 4)};

                using (var command = SqlAccessHelper.CreateCommand(
                    connection, commandText, commandType, commandParameters))
                {
                    command.Parameters["@name"].Value = drugName;
                    command.Parameters["@pharmacyId"].Value = pharmacyId;

                    await connection.OpenAsync();

                    return await SqlAccessHelper.GetEntitiesListAsync<PharmacyDrug>(
                        connection, command, GetDrugFromReader);
                }
            }
        }

        private PharmacyDrug GetDrugFromReader(SqlDataReader reader)
        {
            /** Data columns order : 
             *   0. code        - long
             *   1. name        - string
             *   2. producer    - string
             *   3. quantity    - decimal
             *   4. price       - decical
             *   5. seria       - string
             *   6. barcode     - string
             *   7. prodcode    - int
             */

            //code, name, producer, quantity, price, seria, barcode, prodcode
            var pharmacyDrug = new PharmacyDrug();

            pharmacyDrug.Code = reader.GetFieldValue<long>(0);
            pharmacyDrug.Name = reader.GetFieldValue<string>(1);
            pharmacyDrug.ManufacturerName = reader.GetFieldValue<string>(2);
            pharmacyDrug.Quantity = reader.GetFieldValue<decimal>(3);
            pharmacyDrug.Price = reader.GetFieldValue<decimal>(4);
            pharmacyDrug.Series = reader.TryGetFieldValue<string>(5);
            pharmacyDrug.Barcode = reader.TryGetFieldValue<string>(6);
            pharmacyDrug.ManufacturerCode = reader.GetFieldValue<int>(7);
            
            return pharmacyDrug;
        }
    }
}
