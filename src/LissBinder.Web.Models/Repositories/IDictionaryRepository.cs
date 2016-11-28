using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public interface IDictionaryRepository
    {
        Task<IEnumerable<Models.Drugs.DictionaryDrug>> FindByNameAsync(string drugName);
    }
}
