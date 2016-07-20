using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

namespace Escyug.LissBinder.Models.Services
{
    public interface IPharmacyService
    {
        Task<IEnumerable<PharmacyDrug>> GetDrugsAsync(string drugName);
        Task<bool> BindPharmacyDrugAsync(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug, int phamacyId);
    }
}
