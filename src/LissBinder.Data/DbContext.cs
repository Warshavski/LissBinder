
namespace Escyug.LissBinder.Data
{
    public class DbContext
    {
        public string ConnectionString { get; private set; }

        public DbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
