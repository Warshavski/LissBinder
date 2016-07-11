using System.Threading.Tasks;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Models.Mappings;

namespace Escyug.LissBinder.Models.Repositories
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly IPharmacyByUserQueryProcessor _pharmacyByUserQueryProcessor;

        public PharmacyRepository(IPharmacyByUserQueryProcessor pharmacyByUserQueryProcessor)
        {
            _pharmacyByUserQueryProcessor = pharmacyByUserQueryProcessor;
        }

        public async Task<Pharmacy> GetPharmacyByUserAsync(User user)
        {
            var pharmacyEntity = await _pharmacyByUserQueryProcessor.GetPharmacyAsync(user.Id);

            if (pharmacyEntity != null)
            {
                var pharmacy = PharmacyMappings.EntityToModel(pharmacyEntity);
                return pharmacy;
            }
            else
            {
                return null;
            }
        }
    }
}
