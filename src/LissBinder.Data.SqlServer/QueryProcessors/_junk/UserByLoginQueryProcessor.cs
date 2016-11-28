using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;

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
            var commandText = "dbo.azure_liss_user_select_by_login";
            var commandType = CommandType.StoredProcedure;

            using (var connection = _context.CreateConnection())
            {
                using (var command = _context.CreateCommand(connection, commandText, commandType))
                {
                    command.AddParameter("login", login);

                    var users = await SelectEntityList(command);
                    return users.FirstOrDefault();
                }
            }
        }

    }
}
