using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IPharmacyByUserQueryProcessor
    {
        Task<Pharmacy> GetPharmacyAsync(int userId);
    }
}
