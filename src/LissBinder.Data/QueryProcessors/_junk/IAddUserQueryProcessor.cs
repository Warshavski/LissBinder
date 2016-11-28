using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IAddUserQueryProcessor
    {
        Task<bool> AddUserAsync(User user, int pharmacyId);
    }
}
