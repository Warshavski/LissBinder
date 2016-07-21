using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Escyug.LissBinder.Web.Models.Repositories;
using Escyug.LissBinder.Common.Utils;

namespace Escyug.LissBinder.Web.Models
{
    public sealed class IdentityUserManager : UserManager<User>
    {
        private readonly IUserRepository _userRepository;

        public IdentityUserManager(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<User> FindAsync(string userName, string password)
        {
            var user = await _userRepository.FindByNameAsync(userName);

            if (user != null)
            {
                var masterHash = user.PwdHash;
                var salt = user.Salt;

                // create hash from salt and input password
                // check master hash and input password hash
                // if true create user model
                // else return null

                var compHash = Security.GenerateSaltedHash(
                    Encoding.UTF8.GetBytes(password),
                    salt);

                var isHashMatch = Security.CompareByteArrays(masterHash, compHash);

                if (isHashMatch)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async override Task<IdentityResult> CreateAsync(User user, string password)
        {
            var salt = Security.GenerateSalt();
            var hash = Security.GenerateSaltedHash(
                Encoding.UTF8.GetBytes(password), salt);

            user.PwdHash = hash;
            user.Salt = salt;

            await _userRepository.CreateAsync(user);

            return IdentityResult.Success;

            //return base.CreateAsync(user, password);
        }

    }
}
