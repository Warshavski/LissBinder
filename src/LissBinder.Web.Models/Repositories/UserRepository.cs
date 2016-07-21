using System;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Common.Utils;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserByLoginQueryProcessor _userByLoginQueryProcessor;
        private readonly IAddUserQueryProcessor _userAddQueryProcessor;

        public UserRepository(IUserByLoginQueryProcessor userByLoginQueryProcessor,
            IAddUserQueryProcessor userAddQueryProcessor)
        {
            _userByLoginQueryProcessor = userByLoginQueryProcessor;
            _userAddQueryProcessor = userAddQueryProcessor;
        }


        //---------------------------------------------------------------------


        #region IUserRepository members


        //*** create separate layer for login check
        public async Task<User> GetUserByCredentialsAsync(string login, string password)
        {
            throw new NotImplementedException();
            //var userEntity = await _userByLoginQueryProcessor.GetUserAsync(login);

            //if (userEntity != null)
            //{
            //    var masterHash = userEntity.PwdHash;
            //    var salt = userEntity.Salt;

            //    // create hash from salt and input password
            //    // check master hash and input password hash
            //    // if true create user model
            //    // else return null

            //    var compHash = Security.GenerateSaltedHash(
            //        Encoding.UTF8.GetBytes(password),
            //        Encoding.UTF8.GetBytes(salt));

            //    var isHashMatch = Security.CompareByteArrays(masterHash, compHash);

            //    if (isHashMatch)
            //    {
            //        var user = UserMappings.EntityToModel(userEntity);
            //        return user;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return null;
            //}
        }


        #endregion IUserRepository members


        //---------------------------------------------------------------------


        #region IUserStore<User> members


        public async Task CreateAsync(User user)
        {
            var userEntity = UserMappings.ModelToEntity(user);
            var isUserCreated = await _userAddQueryProcessor.AddUserAsync(userEntity, 1);
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            var userEntity = await _userByLoginQueryProcessor.GetUserAsync(userName);

            if (userEntity != null)
            {
                var user = UserMappings.EntityToModel(userEntity);
                return user;
            }
            else
            {
                return null;
            }
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        #endregion IUserStore<User> members
    }
}
