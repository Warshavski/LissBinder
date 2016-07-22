using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.Entities
{
    public class User
    {
        // represents id_user column
        public int Id {get; set;}

        // represents name column
        public string Name { get; set; }

        // represents login column
        public string Login {get; set;}

        // represents hash column
        public byte[] PwdHash { get; set; }

        // represents salt column
        public byte[] Salt { get; set; }

        // represents liss_pharmacys.id_pharmacy column
        public int PharmacyId { get; set; }
    }
}
