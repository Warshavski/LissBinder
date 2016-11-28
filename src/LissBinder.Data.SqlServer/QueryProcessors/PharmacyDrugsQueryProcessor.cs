using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.Extensions;
using Escyug.LissBinder.Data.QueryProcessors;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class PharmacyDrugsQueryProcessor : BaseQueryProcessor<PharmacyDrug>, IPharmacyDrugsQueryProcessor
    {
        private readonly DbContext _context;

        public PharmacyDrugsQueryProcessor(DbContext context)
        {
            _context = context;
        }


        //---------------------------------------------------------------------


        #region IPharmacyDrugsQueryProcessor member

        public async Task<IEnumerable<PharmacyDrug>> SelectByNameAsync(int pharmacyId, string name)
        {
            var commandText = "dbo.azure_liss_druglist_select_by_name_and_pharmacyid";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("pharmacyId", pharmacyId);
                    command.AddParameter("name", name);

                    await connection.OpenAsync();

                    var pharmacyDrugsList = await base.SelectEntityListAsync(command);

                    return pharmacyDrugsList;
                }
            }
        }

        public async Task<int> ImporDrugsAsync(int pharmacyId, IEnumerable<PharmacyDrug> drugsList)
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await DeleteAllRowsAsync(pharmacyId, connection, transaction);

                        var rowsTotal = drugsList.Count();

                        var drugsData = EntitiesToDataTable(drugsList, pharmacyId);
                        await ExecuteBulkCopyAsync(drugsData, (SqlConnection)connection, (SqlTransaction)transaction);

                        transaction.Commit();

                        return rowsTotal;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                    catch (DbException)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        public async Task<int> ImporDrugsAsync(int pharmacyId, DataTable drugsData)
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await DeleteAllRowsAsync(pharmacyId, connection, transaction);

                        var rowsTotal = drugsData.Rows.Count;

                        await ExecuteBulkCopyAsync(drugsData, (SqlConnection)connection, (SqlTransaction)transaction);

                        transaction.Commit();

                        return rowsTotal;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                    catch (DbException)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        private async Task DeleteAllRowsAsync(int pharmacyId, DbConnection connection, DbTransaction transaction)
        {
            var commandText = "dbo.azure_liss_druglist_delete_by_pharmacyid";
            var commandType = CommandType.StoredProcedure;

            using (var command = _context.CreateCommand(connection, commandText, commandType))
            {
                command.AddParameter("id_pharmacy", pharmacyId);

                command.Transaction = transaction;

                await command.ExecuteNonQueryAsync();
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

        private async Task ExecuteBulkCopyAsync(DataTable data, SqlConnection connection, SqlTransaction transaction)
        {
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
            {
                bulkCopy.BulkCopyTimeout = 1800;
                bulkCopy.DestinationTableName = "liss_drugslist";

                await bulkCopy.WriteToServerAsync(data);
            }
        }

        #endregion IPharmacyDrugsQueryProcessor member


        //---------------------------------------------------------------------


        #region BaseQueryProcessor members

        protected override PharmacyDrug CreateEntity(System.Data.IDataRecord record)
        {
            /** Data columns order : 
            *   0. code        - long
            *   1. name        - string
            *   2. producer    - string
            *   3. quantity    - decimal
            *   4. price       - decimal
            *   5. seria       - string
            *   6. barcode     - string
            *   7. prodcode    - int
            */

            var pharmacyDrug = new PharmacyDrug();

            pharmacyDrug.Code = record.GetValueOrDefault<long>("code");
            pharmacyDrug.Name = record.GetValueOrDefault<string>("name");
            pharmacyDrug.ManufacturerName = record.GetValueOrDefault<string>("producer");
            pharmacyDrug.Quantity = record.GetValueOrDefault<decimal>("quantity");
            pharmacyDrug.Price = record.GetValueOrDefault<decimal>("price");
            pharmacyDrug.Series = record.GetValueOrDefault<string>("seria");
            pharmacyDrug.Barcode = record.GetValueOrDefault<string>("barcode");
            pharmacyDrug.ManufacturerCode = record.GetValueOrDefault<int>("prodcode");

            return pharmacyDrug;
        }

        #endregion BaseQueryProcessor members

    }
}
