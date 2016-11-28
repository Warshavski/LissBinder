using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;


//*** Try to avoid using DataTable instance for SqlBulkCopy (find the way to use IDataReader)
namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class AddPharmacyDrugsQueryProcessor : IAddPharmacyDrugsQueryProcessor
    {
        private readonly DbContext _context;

        public AddPharmacyDrugsQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<int> AddDrugsAsync(IEnumerable<PharmacyDrug> drugsList, int pharmacyId)
        {
            try
            {
                var rowsTotal = drugsList.Count();

                var drugsData = EntitiesToDataTable(drugsList, pharmacyId);
                await ExecuteBulkCopyAsync(drugsData);

                return rowsTotal;
            }
            catch (SqlException)
            {
                return -1;
            }
        }

        private DataTable EntitiesToDataTable(IEnumerable<PharmacyDrug> drugsList, int pharmacyId)
        {
            /**
             * code         - long
             * name         - string
             * producer     - string
             * quantity     - decimal
             * price        - decimal
             * seria        - string
             * barcode      - string
             * prodcode     - int
             * id_pharmacy  - int
             */

            var table = new DataTable();
            table.Columns.Add("code", typeof(long));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("producer", typeof(string));
            table.Columns.Add("quantity", typeof(decimal));
            table.Columns.Add("price", typeof(decimal));
            table.Columns.Add("seria", typeof(string));
            table.Columns.Add("barcode", typeof(string));
            table.Columns.Add("prodcode", typeof(int));
            table.Columns.Add("id_pharmacy", typeof(int));

            foreach (var drug in drugsList)
            {
                table.Rows.Add( 
                    drug.Code,
                    drug.Name,
                    drug.ManufacturerName,
                    drug.Quantity,
                    drug.Price,
                    drug.Series,
                    drug.Barcode,
                    drug.ManufacturerCode,
                    pharmacyId
                    );
            }
           
            return table;
        }

        private async Task ExecuteBulkCopyAsync(DataTable data)
        {
            using (var connection = new SqlConnection(_context.ConnectionString))
            {
                await connection.OpenAsync();
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BulkCopyTimeout = 1800;
                    bulkCopy.DestinationTableName = "liss_drugslist";

                    await bulkCopy.WriteToServerAsync(data);
                }
            }
        }
    }
}
