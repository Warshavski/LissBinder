using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

namespace Escyug.LissBinder.Models.Repositories
{
    public interface IDictionaryDrugsRepository
    {
        Task<IEnumerable<Models.Drugs.DictionaryDrug>> GetDrugsByNameAsync(string drugName);
    }
}
