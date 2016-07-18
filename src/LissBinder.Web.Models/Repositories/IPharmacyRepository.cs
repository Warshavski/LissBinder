using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public interface IPharmacyRepository
    {
        Task<Pharmacy> GetPharmacyByUserAsync(User user);
    }
}
