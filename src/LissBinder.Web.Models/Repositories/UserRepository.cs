using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models;
using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserByLoginQueryProcessor _userByLoginQueryProcessor;

        public UserRepository(IUserByLoginQueryProcessor userByLoginQueryProcessor)
        {
            _userByLoginQueryProcessor = userByLoginQueryProcessor;
        }

        //*** create separate layer for login check
        public async Task<User> GetUserByCredentialsAsync(string login, string password)
        {
            var userEntity = await _userByLoginQueryProcessor.GetUserAsync(login);

            if (userEntity != null)
            {
                var masterHash = userEntity.PwdHash;
                var salt = userEntity.Salt;

                // create hash from salt and input password
                // check master hash and input password hash
                // if true create user model
                // else return null

                var strHash = new StringBuilder();

                using (MD5 md5Hash = MD5.Create())
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    for (int i = 0; i < data.Length; ++i)
                        strHash.Append(data[i].ToString(salt));
                }

                if (string.Compare(masterHash, strHash.ToString()) == 0)
                {
                    var user = UserMappings.EntityToModel(userEntity);
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
    }
}
