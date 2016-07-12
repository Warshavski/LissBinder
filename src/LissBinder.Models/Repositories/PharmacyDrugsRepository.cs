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
        private readonly IAddPharmacyDrugsQueryProcessor _drugsAddQueryProcessor;

        public PharmacyDrugsRepository(
            IPharmacyDrugsByNameQueryProcessor drugByNameQueryProcessor,
            IAddPharmacyDrugsQueryProcessor drugsAddQueryProcessor)
        {
            _drugByNameQueryProcessor = drugByNameQueryProcessor;
            _drugsAddQueryProcessor = drugsAddQueryProcessor;
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

        public async Task<int> AddDrugs(IEnumerable<Drugs.PharmacyDrug> drugsList, int pharmacyId)
        {
            var pharmacyDrugsEntities = new List<Data.Entities.PharmacyDrug>();
            foreach (var model in drugsList)
            {
                pharmacyDrugsEntities.Add(PharmacyDrugMappings.ModelToEntity(model));
            }

            var rowsTotal = await _drugsAddQueryProcessor.AddDrugsAsync(
                pharmacyDrugsEntities, pharmacyId);

            return rowsTotal;
        }
    }
}
