
namespace Escyug.LissBinder.Models.Drugs
{
    public class PharmacyDrug
    {
        public long Code { get; private set; }

        public string Name { get; private set; }

        public string ManufacturerName { get; private set; }

        public decimal Quantity { get; private set; }

        public decimal Price { get; private set; }

        public string Series { get; private set; }

        public string Barcode { get; private set; }

        public int ManufacturerCode { get; private set; }

        public PharmacyDrug(
            long code, string name, string manufacturerName, decimal quantity,
            decimal price, string series, string barcode, int manufacturerCode)
        {
            Code = code;
            Name = name;
            ManufacturerName = manufacturerName;
            Quantity = quantity;
            Price = price;
            Series = series;
            Barcode = barcode;
            ManufacturerCode = manufacturerCode;
        }
    }
}
