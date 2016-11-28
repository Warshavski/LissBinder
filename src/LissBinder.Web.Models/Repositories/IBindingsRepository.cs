using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public interface IBindingsRepository
    {
        Task AddBindingAsync(Models.Binding binding);
    }
}
