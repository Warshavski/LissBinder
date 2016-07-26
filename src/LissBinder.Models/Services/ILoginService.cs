using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Services
{
    public interface ILoginService
    {
        Task<User> SignInAsync(string login, string password);
    }
}
