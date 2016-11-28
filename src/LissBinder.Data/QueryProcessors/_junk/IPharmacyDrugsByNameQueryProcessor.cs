using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IPharmacyDrugsByNameQueryProcessor
    {
        Task<IEnumerable<PharmacyDrug>> GetDrugsAsync(string drugName, int pharmacyId);
    }
}
