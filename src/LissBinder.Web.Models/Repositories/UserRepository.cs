using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data;
using Escyug.LissBinder.Data.SqlServer.DataMappers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escyug.LissBinder.Common.Utils;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public class UserRepository : IUserPasswordStore<User>
    {
        private readonly UserDataMapper _userDataMapper;

        public UserRepository(UserDataMapper userDataMapper)
        {
            _userDataMapper = userDataMapper;
        }

        public Task<string> GetPasswordHashAsync(Models.User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(Models.User user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(Models.User user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Models.User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Models.User user)
        {
            throw new NotImplementedException();
        }

        public Task<Models.User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.User> FindByNameAsync(string userName)
        {
            var user = await _userDataMapper.SelectByLogin(userName);

            return user;
        }

        public Task UpdateAsync(Models.User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
