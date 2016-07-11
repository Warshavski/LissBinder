using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByCredentialsAsync(string login, string password);
    }
}
