using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data.Entities;
using Escyug.LissBinder.Data.QueryProcessors;
using System.Data.SqlClient;
using System.Data;
using Escyug.LissBinder.Data.SqlServer.Common;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class AddUserQueryProcessor : IAddUserQueryProcessor
    {
        private readonly DbContext _context;

        public AddUserQueryProcessor(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserAsync(User user, int pharmacyId)
        {
            using (var connection = new SqlConnection(_context.ConnectionString))
            {
                await connection.OpenAsync();

                var transaction = connection.BeginTransaction("UserCreateTransaction");

                try
                {
                    // USER COMMAND SECTION
                    //-----------------------------------------------

                    var userCommandText = "dbo.azure_liss_user_create";
                    var userCommandType = CommandType.StoredProcedure;
                    var userCommandParameters = new SqlParameter[] {
                        new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, 
                            ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                         new SqlParameter("@login", SqlDbType.NVarChar, 50),
                         new SqlParameter("@name", SqlDbType.NVarChar, 250),
                         new SqlParameter("@hash", SqlDbType.Binary, 32),
                         new SqlParameter("@salt", SqlDbType.Binary, 32)};

                    var userId = -1;
                    using (var userCommand = SqlAccessHelper.CreateCommand(
                        connection, userCommandText, userCommandType, userCommandParameters, transaction))
                    {
                        userCommand.Parameters["@login"].Value = user.Login;
                        userCommand.Parameters["@name"].Value = user.Name;
                        userCommand.Parameters["@hash"].Value = user.PwdHash;
                        userCommand.Parameters["@salt"].Value = user.Salt;

                        userId = (int)await userCommand.ExecuteScalarAsync();
                    }

                    // PHARMACY COMMAND SECTION
                    //-----------------------------------------------

                    var pharmacyCommandText = "dbo.azure_liss_user_pharmacy_create";
                    var pharmacyCommandType = CommandType.StoredProcedure;
                    var pharmacyCommandParameters = new SqlParameter[] {
                        new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, 
                            ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", DataRowVersion.Current, null),
                         new SqlParameter("@id_pharmacy", SqlDbType.Int, 4),
                         new SqlParameter("@id_user", SqlDbType.Int, 4)};
                    using (var pharmacyCommand = SqlAccessHelper.CreateCommand(
                        connection, pharmacyCommandText, pharmacyCommandType, pharmacyCommandParameters, transaction))
                    {
                        pharmacyCommand.Parameters["@id_pharmacy"].Value = pharmacyId;
                        pharmacyCommand.Parameters["@id_user"].Value = userId;

                        await pharmacyCommand.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    return false;
                }
                return true;
            }

        }
    }
}
