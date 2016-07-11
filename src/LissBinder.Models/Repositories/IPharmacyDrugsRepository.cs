using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Repositories
{
    public interface IPharmacyDrugsRepository
    {
        Task<IEnumerable<Models.Drugs.PharmacyDrug>> GetDrugsByNameAsync(string drugName, int pharmacyId);
    }
}
