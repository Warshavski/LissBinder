using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Web.Models.Drugs;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public interface IDictionaryDrugsRepository
    {
        Task<IEnumerable<Models.Drugs.DictionaryDrug>> GetDrugsByNameAsync(string drugName);
    }
}
