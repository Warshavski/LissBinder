using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    //*** inject pharmacy 
    public interface IPharmacyDrugsQueryProcessor
    {
        Task<IEnumerable<PharmacyDrug>> SelectByNameAsync(int pharmacyId, string name);
        Task<int> ImporDrugsAsync(int pharmacyId, IEnumerable<PharmacyDrug> drugsList);
    }
}
