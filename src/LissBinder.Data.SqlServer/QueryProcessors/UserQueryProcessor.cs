using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public sealed class UserQueryProcessor : BaseQueryProcessor<User>, IUserQueryProcessor
    {
        private readonly DbContext _context;

        public UserQueryProcessor(DbContext context)
        {
            _context = context;
        }


        ////---------------------------------------------------------------------


        #region IUserQueryProcessor members

        public async Task<User> SelectByNameAsync(string login)
        {
            var commandText = "dbo.azure_liss_user_select_by_name";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("name", login);

                    var users = await base.SelectEntityListAsync(command);
                    return users != null ? users.FirstOrDefault() : null;
                }
            }
        }

        public async Task<User> SelectByIdAsync(int id)
        {
            var commandText = "dbo.azure_liss_user_select_by_id";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("id_user", id);

                    var users = await base.SelectEntityListAsync(command);
                    return users.FirstOrDefault();
                }
            }
        }

        public async Task<IEnumerable<User>> SelectAllAsync()
        {
            var commandText = "SELECT * FROM dbo.[liss_users]";
            var commandType = CommandType.Text;

            using (var connection = _context.CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    var usersList = await base.SelectEntityListAsync(command);
                    return usersList;
                }
            }
        }

        public async Task InsertAsync(User entity)
        {
            var commandText = "dbo.azure_liss_user_create";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                /*
                 * @pharmacyId int,
                 * @login nvarchar(50),
                 * @name nvarchar(250),
                 * @hash binary(32),
                 * @salt binary(32)
                 * 
                 */

                await connection.OpenAsync();

                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("pharmacyId", entity.PharmacyId);
                    command.AddParameter("name", entity.Name);
                    command.AddParameter("name_description", entity.NameDescription);
                    command.AddParameter("hash", entity.PasswordHash);
                    //command.AddParameter("salt", entity.Salt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        #endregion IUserQueryProcessor members


        ////---------------------------------------------------------------------


        #region BaseQueryProcessor members

        protected override User CreateEntity(IDataRecord record)
        {
            /** DATA FIELDS
             * 
             * 0. [id_user] - int
             * 1. [name]    - string 
             * 2. [login]   - string 
             * 3. [hash]    - byte[32]
             * 4. [salt]    - byte[32]
             * 
             */
            var user = new User();

            user.Id = (int)record["id_user"];
            user.Name = (string)record["name"];
            user.NameDescription = (string)record["name_description"];
            user.PasswordHash = (string)record["hash"];
            user.PharmacyId = (int)record["id_pharmacy"];
            //user.Salt = (byte[])record["salt"];

            return user;
        }

        #endregion BaseQueryProcessor members

    }
}
