using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.Extensions
{
    public static class DbCommandExtensions
    {
        public static void AddParameter(this DbCommand command, string name, object value)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
        }
    }

    
}
