using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Escyug.LissBinder.Web.Models.Repositories;
using Escyug.LissBinder.Common.Utils;
using Escyug.LissBinder.Data;

namespace Escyug.LissBinder.Web.Models.Services
{
    public sealed class UserService : UserManager<User>
    {
        private readonly IUserStore<User> _userRepository;

        public UserService(IUserStore<User> userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
