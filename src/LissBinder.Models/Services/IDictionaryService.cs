using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

namespace Escyug.LissBinder.Models.Services
{
    public interface IDictionaryService
    {
        Task<IEnumerable<DictionaryDrug>> GetDrugsAsync(string drugName);
    }
}
