using System;
using System.Data;
using System.Data.Common;

namespace Escyug.LissBinder.Data
{
    public abstract class DbContext
    {
        public string ConnectionString { get; private set; }

        public string ProviderName { get; private set; }

        //public DbConnection Conneciton { get; private set; }

        public DbContext(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        
        // DB CONNECTION CREATE SECTION
        //---------------------------------------------------------------------

        public DbConnection CreateConnection()
        {
            // Assume failure.
            DbConnection connection = null;

            // Create the DbProviderFactory and DbConnection.
            if (ConnectionString != null)
            {
                try
                {
                    DbProviderFactory factory =
                        DbProviderFactories.GetFactory(ProviderName);

                    connection = factory.CreateConnection();
                    connection.ConnectionString = ConnectionString;
                }
                catch (DbException)
                {
                    // Set the connection to null if it was created.
                    if (connection != null)
                    {
                        connection = null;
                    }
                }
            }
            
            return connection;
        }



        // DB COMMAND CREATE SECTION
        //---------------------------------------------------------------------

        //public DbCommand CreateCommand(DbConnection connection, string commandText,
        //   CommandType commandType, DbParameter[] parameters, DbTransaction transaction)
        //{
        //    throw new NotImplementedException();
        //}

        public DbCommand CreateCommand(DbConnection connection,
           string commandText, CommandType commandType, DbParameter[] parameters)
        {
            DbCommand command = CreateCommand(connection, commandText, commandType);
            command.Parameters.AddRange(parameters);

            return command;
        }

        public DbCommand CreateCommand(DbConnection connection, 
            string commandText, CommandType commandType)
        {
            DbCommand command = CreateCommand(connection, commandText);

            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }

        private DbCommand CreateCommand(DbConnection connection, string commandText)
        {
            DbCommand command = null;
            if (commandText != null)
            {
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = commandText;
                }
                catch (DbException)
                {
                    // Set the command to null if it was created.
                    if (command != null)
                    {
                        command = null;
                    }
                }
            }

            return command;
        }
    }
}
