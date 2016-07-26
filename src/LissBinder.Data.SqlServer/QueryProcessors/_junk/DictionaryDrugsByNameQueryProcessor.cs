using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Data.SqlServer.Common;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class DictionaryDrugsByNameQueryProcessor : IDictionaryDrugsByNameQueryProcessor
    {
        private readonly DbContext _context;

        public DictionaryDrugsByNameQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.DictionaryDrug>> GetDrugsAsync(string drugName)
        {
            using (var connection = new SqlConnection(_context.ConnectionString))
            {
                var commandText = "dbo.azure_rls_dictionary_select_by_drugname_opt";
                var commandType = CommandType.StoredProcedure;
                var commandParameters = new SqlParameter[] {
                    new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                    new SqlParameter("@name", SqlDbType.NVarChar, 200) };

                using (var command = SqlAccessHelper.CreateCommand(
                    connection, commandText, commandType, commandParameters))
                {
                    command.Parameters["@name"].Value = drugName;

                    await connection.OpenAsync();

                    return await SqlAccessHelper.GetEntitiesListAsync<DictionaryDrug>(
                        connection, command, GetDrugFromReader);
                }
            }
        }

        private DictionaryDrug GetDrugFromReader(SqlDataReader reader)
        {
            /** Data columns order : 
             *   0. INAME           - string
             *   1. DRUGFORMDESC    - string
             *   2. PREPID          - int
             *   3. NOMENID         - int
             *   4. DESCID          - int
             *   5. DRUGFORMID      - int
             */

            var dictionaryDrug = new DictionaryDrug();

            dictionaryDrug.Name = reader.GetFieldValue<string>(0);
            dictionaryDrug.DrugformDescription = reader.GetFieldValue<string>(1);
            dictionaryDrug.PrepId = reader.GetFieldValue<int>(2);
            dictionaryDrug.NomenId = reader.GetFieldValue<int>(3);
            dictionaryDrug.DescriptionId = reader.GetFieldValue<int>(4);
            dictionaryDrug.DrugformId = reader.GetFieldValue<int>(5);

            return dictionaryDrug;
        }
    }
}
