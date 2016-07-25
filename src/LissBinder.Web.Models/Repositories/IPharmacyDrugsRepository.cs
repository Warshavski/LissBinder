using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Web.Models.Drugs;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public interface IPharmacyDrugsRepository
    {
        Task<IEnumerable<PharmacyDrug>> FindByNameAsync(int pharmacyId, string drugName);
        Task<int> ImportAsync(IEnumerable<PharmacyDrug> drugsList);
    }
}
