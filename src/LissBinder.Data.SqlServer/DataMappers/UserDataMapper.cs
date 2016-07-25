using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data;

using Escyug.LissBinder.Data;
using Escyug.LissBinder.Data.Entities;

namespace Escyug.LissBinder.Data.SqlServer.DataMappers
{
    public class UserDataMapper : DataMapper<User>
    {
        private readonly DbContext _context;

        public UserDataMapper(DbContext context)
        {
            _context = context;
        }

        public async Task<User> SelectByLogin(string login)
        {
            var commandText = "dbo.azure_liss_user_select_by_login";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("login", login);

                    var users= await SelectEntityList(command);
                    return users.FirstOrDefault();
                }
            }
        }

        public async override Task<IEnumerable<User>> SelectAllAsync()
        {
            var commandText = "SELECT * FROM dbo.[liss_users]";
            var commandType = CommandType.Text;

            using (var connection = _context.CreateConnection())
            {
                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                   var usersList = await SelectEntityList(command);
                   return usersList;
                }
            }
        }

        public async override Task InsertAsync(User entity)
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
                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("pharmacyId", entity.PharmacyId);
                    command.AddParameter("login", entity.Login);
                    command.AddParameter("name", entity.Name);
                    command.AddParameter("hash", entity.PwdHash);
                    command.AddParameter("salt", entity.Salt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public override Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

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
            user.Login = (string)record["login"];
            user.PwdHash = (byte[])record["hash"];
            user.Salt = (byte[])record["salt"];

            return user;
        }
    }
}
