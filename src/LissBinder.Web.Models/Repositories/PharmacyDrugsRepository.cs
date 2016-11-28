using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public sealed class PharmacyDrugsRepository : IPharmacyDrugsRepository
    {
        private readonly IPharmacyDrugsQueryProcessor _drugsQueryProcessor;

        public PharmacyDrugsRepository(IPharmacyDrugsQueryProcessor drugsQueryProcessor)
        {
            _drugsQueryProcessor = drugsQueryProcessor;
        }


        public async Task<IEnumerable<Drugs.PharmacyDrug>> FindByNameAsync(int pharmacyId, string drugName)
        {
            var entitiesList = await _drugsQueryProcessor.SelectByNameAsync(pharmacyId, drugName);

            if (entitiesList == null)
            {
                throw new ArgumentNullException("pharmacy drugs");
            }

            var drugsList = new List<Models.Drugs.PharmacyDrug>();
            foreach (var entity in entitiesList)
            {
                drugsList.Add(PharmacyDrugMappings.EntityToModel(entity));
            }

            return drugsList;
        }

        public async Task<int> ImportAsync(int pharmacyId, IEnumerable<Drugs.PharmacyDrug> drugsList)
        {
            var drugsData = PharmacyDrugMappings.ModelToDataTable(drugsList, pharmacyId);

            var rowsImport = await _drugsQueryProcessor.ImporDrugsAsync(pharmacyId, drugsData);

            return rowsImport;
        }
    }
}
