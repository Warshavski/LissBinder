using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.Extensions;
using Escyug.LissBinder.Data.QueryProcessors;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public sealed class DictionaryQueryProcessor : BaseQueryProcessor<DictionaryDrug>, IDictionaryQueryProcessor
    {
        private readonly DbContext _context;

        public DictionaryQueryProcessor(DbContext context)
        {
            _context = context;
        }


        //---------------------------------------------------------------------


        #region IDictionaryQueryProcessor members

        public async Task<IEnumerable<DictionaryDrug>> SelectByNameAsync(string name)
        {
            var commandText = "dbo.azure_rls_dictionary_select_by_drugname_opt";
            var commandType = CommandType.StoredProcedure;

            using (var conneciton = _context.CreateConnection())
            {
                using (var command = _context.CreateCommand(conneciton, commandText, commandType))
                {
                    command.AddParameter("name", name);

                    await conneciton.OpenAsync();

                    var drugsList = await base.SelectEntityListAsync(command);

                    // check count on 0 elements

                    return drugsList;
                }
            }

        }

        #endregion IDictionaryQueryProcessor members


        //---------------------------------------------------------------------


        #region BaseQueryProcessor members

        protected override DictionaryDrug CreateEntity(System.Data.IDataRecord record)
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

            dictionaryDrug.Name = (string)record["INAME"];
            dictionaryDrug.DrugformDescription = (string)record["DRUGFORMDESC"];
            dictionaryDrug.PrepId = (int)record["PREPID"];
            dictionaryDrug.NomenId = (int)record["NOMENID"];
            dictionaryDrug.DescriptionId = (int)record["DESCID"];
            dictionaryDrug.DrugformId = (int)record["DRUGFORMID"];

            return dictionaryDrug;

            throw new NotImplementedException();
        }

        #endregion BaseQueryProcessor members
    }
}
