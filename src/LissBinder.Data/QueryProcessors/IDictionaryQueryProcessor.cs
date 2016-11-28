using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IDictionaryQueryProcessor
    {
        Task<IEnumerable<DictionaryDrug>> SelectByNameAsync(string name);
    }
}
