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
        private readonly IAddBindingQueryProcessor _bindingAddQueryProcessor;

        public PharmacyDrugsRepository(
            IPharmacyDrugsByNameQueryProcessor drugByNameQueryProcessor,
            IAddPharmacyDrugsQueryProcessor drugsAddQueryProcessor,
            IAddBindingQueryProcessor bindingAddQueryProcessor)
        {
            _drugByNameQueryProcessor = drugByNameQueryProcessor;
            _drugsAddQueryProcessor = drugsAddQueryProcessor;
            _bindingAddQueryProcessor = bindingAddQueryProcessor;
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

        public async Task<int> AddDrugsAsync(IEnumerable<Drugs.PharmacyDrug> drugsList, int pharmacyId)
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


        public async Task<bool> AddBindingAsync(Models.Binding binding)
        {
            var bindingEntity = BindingMappings.ModelToEntity(binding);

            return await _bindingAddQueryProcessor.AddBindingAsync(bindingEntity);
        }
    }
}
