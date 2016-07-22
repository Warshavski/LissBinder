using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Services
{
    public interface ILoginService
    {
        Task<ServiceToken> SignInAsync(string login, string password);
    }
}
