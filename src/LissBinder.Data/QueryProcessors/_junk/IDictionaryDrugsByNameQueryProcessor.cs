using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IDictionaryDrugsByNameQueryProcessor
    {
        Task<IEnumerable<DictionaryDrug>> GetDrugsAsync(string drugName);
    }
}
