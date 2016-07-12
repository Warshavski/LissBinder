using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Data.SqlServer.Common;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class UserByLoginQueryProcessor : IUserByLoginQueryProcessor
    {
        private readonly DbContext _context;

        public UserByLoginQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string login)
        {
            using (var connection = new SqlConnection(_context.ConnectionString))
            {
                var commandText = "dbo.azure_liss_user_select_by_login";
                var commandType = CommandType.StoredProcedure;
                var commandParameters = new SqlParameter[] {
                    new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, 
                        ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                    new SqlParameter("@login", SqlDbType.NVarChar, 50)};

                using (var command = SqlAccessHelper.CreateCommand(
                    connection, commandText, commandType, commandParameters))
                {
                    command.Parameters["@login"].Value = login;

                    await connection.OpenAsync();

                    return await SqlAccessHelper.GetEntityAsync<User>(
                        connection, command, GetUserFromReader);
                }
            }
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            /** Data columns order : 
             *   0. id_user - int
             *   1. name    - string
             *   2. login   - string
             *   3. pwd     - string
             *   4. datakey - string
             */

            var user = new User();

            user.Id = reader.GetFieldValue<int>(0);
            user.Name = reader.GetFieldValue<string>(1);
            user.Login = reader.GetFieldValue<string>(2);
            user.PwdHash = reader.GetFieldValue<string>(3);
            user.Salt = reader.GetFieldValue<string>(4);

            return user;
        }

    }
}
