using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Mappings;

namespace Escyug.LissBinder.Models.Repositories
{
    public class DictionaryDrugsRepository : IDictionaryDrugsRepository
    {
        private IDictionaryDrugsByNameQueryProcessor _dictionaryDrugsByNameProcessor;

        public DictionaryDrugsRepository(IDictionaryDrugsByNameQueryProcessor dictionaryDrugsByNameProcessor)
        {
            _dictionaryDrugsByNameProcessor = dictionaryDrugsByNameProcessor;
        }
   
        public async Task<IEnumerable<Models.Drugs.DictionaryDrug>> GetDrugsByNameAsync(string drugName)
        {
            var dictionaryDrugsEntities = await _dictionaryDrugsByNameProcessor.GetDrugsAsync(drugName);

            if (dictionaryDrugsEntities != null)
            {
                var dictionaryDrugsList = new List<Models.Drugs.DictionaryDrug>();
                foreach (var entity in dictionaryDrugsEntities)
                {
                    dictionaryDrugsList.Add(DictionaryDrugMappings.EntityToModel(entity));
                }

                return dictionaryDrugsList;
            }
            else
            {
                return null;
            }
        }
    }
}
