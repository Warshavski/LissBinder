using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.Entities
{
    public class PharmacyDrug
    {
        // represents pharmacy drug code
        public long Code { get; set; }

        // represents pharmacy drug name
        public string Name { get; set; }

        // represents pharmacy drug manufacturer name
        public string ManufacturerName { get; set; }

        // repsenets drug quantity
        public decimal Quantity { get; set; }

        // repsenets drug price
        public decimal Price { get; set; }

        // represents drug series
        public string Series { get; set; }

        // represents drug barcode (EAN13)
        public string Barcode { get; set; }

        // represents pharmacy drug manufacturer code
        public int ManufacturerCode { get; set; }

    }
}
