using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.Entities
{
    public class DictionaryDrug
    {
        public string Name { get; set; }

        public string DrugformDescription { get; set; }

        public int NomenId { get; set; }

        public int PrepId { get; set; }

        public int DescriptionId { get; set; }

        public int DrugformId { get; set; }
    }
}
