using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public class UserRepository : IUserStore<User>, IUserPasswordStore<User>
    {
        private readonly IUserQueryProcessor _userQueryProcessor;

        public UserRepository(IUserQueryProcessor userQueryProcessor)
        {
            _userQueryProcessor = userQueryProcessor;
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SetPasswordHash(passwordHash);

            return Task.FromResult(0);
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var userEntity = UserMappings.ModelToEntity(user);
            
            await _userQueryProcessor.InsertAsync(userEntity);
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            var userEntity = await _userQueryProcessor.SelectByIdAsync(int.Parse(userId));

            return ConvertToModel(userEntity);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            var userEntity = await _userQueryProcessor.SelectByNameAsync(userName);

            return ConvertToModel(userEntity);
        }   

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        //---------------------------------------------------------------------


        #region Helper methods 

        private User ConvertToModel(Data.Entities.User userEntity)
        {
            if (userEntity != null)
            {
                var userModel = UserMappings.EntityToModel(userEntity);

                return userModel;
            }
            else
            {
                return null;
            }
        }

        #endregion Helper methods
    }
}
