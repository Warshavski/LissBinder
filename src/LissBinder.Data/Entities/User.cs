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

        // represents pwd column
        public string PwdHash {get; set;}

        // represents datakey column
        public string Salt {get; set;} 
    }
}
