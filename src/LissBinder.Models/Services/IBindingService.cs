using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

namespace Escyug.LissBinder.Models.Services
{
    public interface IBindingService
    {
        Task<bool> BindAsync(Binding binding);
        Task<bool> BindAsync(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug);
    }
}
