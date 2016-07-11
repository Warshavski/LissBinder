using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data;
using Escyug.LissBinder.Data.QueryProcessors;

namespace Escyug.LissBinder.Data.SqlServer.QueryProcessors
{
    public class AddPharmacyDrugsQueryProcessor 
    {
        private readonly DbContext _context;

        public AddPharmacyDrugsQueryProcessor(DbContext context)
        {
            _context = context;
        }
    }
}
