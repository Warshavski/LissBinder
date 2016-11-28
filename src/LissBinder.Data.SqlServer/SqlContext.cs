namespace Escyug.LissBinder.Data.SqlServer
{
    public class SqlContext : DbContext
    {
        private const string PROVIDER_NAME = "System.Data.SqlClient";

        public SqlContext(string connectionString)
            : base(connectionString, PROVIDER_NAME)
        {

        }
    }
}
