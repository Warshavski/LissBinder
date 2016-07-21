using System.Threading.Tasks;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
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
            var id = int.Parse(user.Id);
            var pharmacyEntity = await _pharmacyByUserQueryProcessor.GetPharmacyAsync(id);

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
