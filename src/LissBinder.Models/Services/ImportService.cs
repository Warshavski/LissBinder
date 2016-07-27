using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Metadata;
using Escyug.LissBinder.Models.Utils;
using Escyug.LissBinder.Models.Services.Common;

namespace Escyug.LissBinder.Models.Services
{
    /// <summary>
    /// SqlDataReader extension for DBNull value check.
    /// </summary>
    public static class OleDbDataReaderExtension
    {
        /// <summary>
        /// Try to get value from SqlDataReader column(check for DBNull value).
        /// </summary>
        /// <typeparam name="TValue">Data type of column.</typeparam>
        /// <param name="reader">SqlDataReader instance.</param>
        /// <param name="columnIndex">Column index from SqlDataReader.</param>
        /// <returns>SqlDataReader column value and default data type value if column is DBNull</returns>
        public static TValue TryGetFieldValue<TValue>(this DbDataReader reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
            {
                return default(TValue);
            }
            else
            {
                return reader.GetFieldValue<TValue>(columnIndex);
            }
        }
    }

    public class ImportService : IImportService
    {
        private readonly string _apiUri;

        public ImportService(string apiUri)
        {
            _apiUri = apiUri;
        }



        // METADATA SECTION
        //---------------------------------------------------------------------

        
        /// <summary>
        /// Get file metadata.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TableMetadata>> GetFileMetadataAsync(string connectionString)
        {
            var tablesMetadataList = new List<TableMetadata>();

            //throw new NotImplementedException();
            using (var connection = new OleDbConnection(connectionString))
            {
                await connection.OpenAsync();

                var tablesSchema = connection.GetSchema("Tables");
                foreach (var tablesSchemaRow in tablesSchema.AsEnumerable())
                {
                    var tableName = tablesSchemaRow["TABLE_NAME"].ToString();
                    var rowsCount = await CountRows(tableName, connection);

                    var columnsMetadata = GetColumnsMetadata(connection, tableName);

                    tablesMetadataList.Add(new TableMetadata(tableName, columnsMetadata, rowsCount));
                }
            }

            return tablesMetadataList;
        }

        /// <summary>
        /// Count total rows in table.
        /// </summary>
        /// <param name="connectionString">Open OleDbConnection to file.</param>
        /// <returns>Total rows as int.</returns>
        private async Task<int> CountRows(string tableName, OleDbConnection connection)
        {
            try
            {
                var commandText = "SELECT COUNT(*) AS TOTAL_ROWS FROM [" + tableName + "]";
                using (var command = new OleDbCommand(commandText, connection))
                {
                    var count = await command.ExecuteScalarAsync();

                    return (int)count;
                }
            }
            catch (DbException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Get columns metadata.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private IEnumerable<ColumnMetadata> GetColumnsMetadata(DbConnection connection, string tableName)
        {
            var columnsMetadataList = new List<ColumnMetadata>();

            var columnsSchema = connection.GetSchema("Columns", new string[] { null, null, tableName, null });
            foreach (var columnsSchemaRow in columnsSchema.AsEnumerable())
            {
                var columnName = columnsSchemaRow["COLUMN_NAME"].ToString();
                var columnDataType = columnsSchemaRow["DATA_TYPE"].ToString();

                var columnLength = -1;
                int.TryParse(columnsSchemaRow["CHARACTER_MAXIMUM_LENGTH"].ToString(), out columnLength);

                columnsMetadataList.Add(new ColumnMetadata(columnName, columnDataType, columnLength));
            }

            return columnsMetadataList;
        }



        // IMPORT SECTION
        //---------------------------------------------------------------------


        /// <summary>
        /// Import data from OleDb file to MS SQL Server
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="pharmacyId"></param>
        /// <returns></returns>
        public async Task<int> ImportAsync(string connectionString, string tableName)
        {
            var rowsCopied = 0;

            using (var connection = new OleDbConnection(connectionString))
            {
                var commandText = "SELECT * FROM [" + tableName + "]";

                await connection.OpenAsync();

                var drugsList = new List<PharmacyDrug>();
                using (var command = new OleDbCommand(commandText, connection))
                {
                    DataTable data = new DataTable("WATTEST");
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //data.Load(reader);
                        while (await reader.ReadAsync())
                        {
                            var drug = GetDrugFromReader(reader);
                            drugsList.Add(drug);
                        }

                        var responseAddress = "api/drugs/";

                        var accessToken = ApiContext.Token.AccessToken;

                        rowsCopied =
                            await HttpHelper.PostEntityAsync<int, List<PharmacyDrug>>(_apiUri, responseAddress, accessToken, drugsList);
                    }
                }  
            }

            return rowsCopied;
        }

        /// <summary>
        /// Parse PharmacyDrug object from DbDataRe ader.
        /// </summary>
        /// <param name="reader">DbDataReader</param>
        /// <returns>PharmacyDrug object.</returns>
        private PharmacyDrug GetDrugFromReader(DbDataReader reader)
        {
            //code, name, producer, quantity, price, seria, barcode, prodcode, id_drugstore

            var drugCode = (int)reader.GetFieldValue<double>(0);
            var drugName = reader.TryGetFieldValue<string>(1);
            var manufactorerName = reader.TryGetFieldValue<string>(2);
            var drugQnt = (decimal)reader.TryGetFieldValue<double>(3);
            var drugPrice = (decimal)reader.TryGetFieldValue<double>(4);
            var drugSeries = reader.TryGetFieldValue<string>(5);
            var drugBarcode = reader.TryGetFieldValue<string>(6);
            var manufactorerCode = (int)reader.TryGetFieldValue<double>(7);
            
            return new Models.Drugs.PharmacyDrug(
                drugCode, drugName, manufactorerName, drugQnt,
                drugPrice, drugSeries, drugBarcode, manufactorerCode);
        }
    }
}
