using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IAddPharmacyDrugsQueryProcessor
    {
        Task<int> AddDrugsAsync(IEnumerable<PharmacyDrug> drugsList, int pharmacyId);
    }
}
