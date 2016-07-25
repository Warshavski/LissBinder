using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly IDictionaryQueryProcessor _dictionaryQueryProcessor;

        public async Task<IEnumerable<Drugs.DictionaryDrug>> FindByNameAsync(string drugName)
        {
            var drugsEntitiesList = await _dictionaryQueryProcessor.SelectByNameAsync(drugName);

            if (drugsEntitiesList == null)
            {
                throw new ArgumentNullException("dictionary drug");
            }

            var drugsList = new List<Drugs.DictionaryDrug>();
            foreach (var entity in drugsEntitiesList)
            {
                drugsList.Add(DictionaryMappings.EntityToModel(entity));
            }

            return drugsList;
        }
    }
}
