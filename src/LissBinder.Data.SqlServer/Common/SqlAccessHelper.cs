using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.SqlServer.Common
{
    /// <summary>
    /// SqlDataReader extension for DBNull value check.
    /// </summary>
    public static class SqlDataReaderExtension
    {
        /// <summary>
        /// Try to get value from SqlDataReader column(check for DBNull value).
        /// </summary>
        /// <typeparam name="TValue">Data type of column.</typeparam>
        /// <param name="reader">SqlDataReader instance.</param>
        /// <param name="columnIndex">Column index from SqlDataReader.</param>
        /// <returns>SqlDataReader column value and default data type value if column is DBNull</returns>
        public static TValue TryGetFieldValue<TValue>(this SqlDataReader reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
            {
                return default(TValue);
            }
            else
            {
                return reader.GetFieldValue<TValue>(columnIndex);
            }
        }
    }

    /// <summary>
    /// Sql data access helper class.
    /// Creates SqlConnection, SqlCommand, and get values from SqlDataReader.
    /// </summary>
    internal sealed class SqlAccessHelper
    {
        /// <summary>
        /// Creates DbCommand instance.
        /// </summary>
        /// <param name="connection">SqlConnection which used to access database</param>
        /// <param name="commandText">Command text</param>
        /// <param name="commandType">CommandType type of sql command (text, stored procedure)</param>
        /// <param name="parameters">SqlParameters array of parameters for sql command</param>
        /// <returns>DbCommand instance</returns>
        public static SqlCommand CreateCommand(SqlConnection connection, string commandText,
            CommandType commandType, SqlParameter[] parameters)
        {
            SqlCommand command = null;

            if (commandText != null)
            {
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = commandText;
                    command.CommandType = commandType;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                }
                catch (SqlException)
                {
                    // Set the command to null if it was created.
                    if (command != null)
                        command = null;
                }
            }
            // Return the connection.
            return command;
        }

        /// <summary>
        /// Creates a list of TEntities(generic) from the current sql query result set (async)
        /// </summary>
        /// <param name="connection">SqlConnection </param>
        /// <param name="command">SqlCommnad </param>
        /// <param name="ReaderParser">Func<SqlDataReader, TEntitie> </param>
        /// <returns>A list of TEntitie filled from the sql query result set</returns>
        public static async Task<IEnumerable<TEntity>> GetEntitiesListAsync<TEntity>(
            SqlConnection connection, SqlCommand command, Func<SqlDataReader, TEntity> ReaderParser)
            where TEntity : class
        {

           /* 
            * Since none of the rows are likely to be large, 
            * we will execute this without specifying a CommandBehavior
            * This will cause the default (non-sequential) access mode to be used
            */
            using (var reader = await command.ExecuteReaderAsync())
            {
                var entitiesList = new List<TEntity>();

                while (await reader.ReadAsync())
                {
                    var entitie = ReaderParser(reader);
                    entitiesList.Add(entitie);
                }

                return entitiesList;
            }
        }

        /// <summary>
        /// Creates a TEntity(generic) from the current sql query result set (async)
        /// </summary>
        /// <param name="connection">SqlConnection </param>
        /// <param name="command">SqlCommnad </param>
        /// <param name="ReaderParser">Func<SqlDataReader, TEntity> </param>
        /// <returns>A TEntity filled from the sql query result set</returns>
        public static async Task<TEntity> GetEntityAsync<TEntity>(
            SqlConnection connection, SqlCommand command, Func<SqlDataReader, TEntity> ReaderParser)
            where TEntity : class
        {

           /* 
            * Since none of the rows are likely to be large, 
            * we will execute this without specifying a CommandBehavior
            * This will cause the default (non-sequential) access mode to be used
            */
            using (var reader = await command.ExecuteReaderAsync())
            {
                // Always use ReadAsync
                if (await reader.ReadAsync())
                {
                    var entitie = ReaderParser(reader);
                    return entitie;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
