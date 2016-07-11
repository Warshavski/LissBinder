using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Repositories
{
    public interface IPharmacyRepository
    {
        Task<Pharmacy> GetPharmacyByUserAsync(User user);
    }
}
