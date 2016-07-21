using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public interface IUserRepository : IUserStore<User>
    {
        Task<User> GetUserByCredentialsAsync(string login, string password);
    }
}
