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
    public class PharmacyDrugsRepository : IPharmacyDrugsRepository
    {
        private readonly IPharmacyDrugsByNameQueryProcessor _drugByNameQueryProcessor;

        public PharmacyDrugsRepository(IPharmacyDrugsByNameQueryProcessor drugByNameQueryProcessor)
        {
            _drugByNameQueryProcessor = drugByNameQueryProcessor;
        }

        public async Task<IEnumerable<Drugs.PharmacyDrug>> GetDrugsByNameAsync(string drugName, int pharmacyId)
        {
            var pharmacyDrugsEntities = await _drugByNameQueryProcessor.GetDrugsAsync(drugName, pharmacyId);

            if (pharmacyDrugsEntities != null)
            {
                var pharmacyDrugsList = new List<Models.Drugs.PharmacyDrug>();
                foreach (var entity in pharmacyDrugsEntities)
                {
                    pharmacyDrugsList.Add(PharmacyDrugMappings.EntityToModel(entity));
                }

                return pharmacyDrugsList;
            }
            else
            {
                return null;
            }
        }
    }
}
