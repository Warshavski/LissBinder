using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.Entities
{
    public class Binding
    {
        public long PharmacyDrugCode { get; set; }

        public int PharmacyDrugProdCode { get; set; }

        public int DescriptionId { get; set; }

        public int DrugformId { get; set; }

        public int NomenId { get; set; }

        public int PrepId { get; set; }

        public int PharmacyId { get; set; }
    }
}
