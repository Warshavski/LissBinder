
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
            long drugCode, string drugName, string manufacturerName, decimal drugQnt, 
            decimal drugPrice, string drugSeries, string drugBarcode, int manufacturerCode)
        {
            Code = drugCode;
            Name = drugName;
            ManufacturerName = manufacturerName;
            Quantity = drugQnt;
            Price = drugPrice;
            Series = drugSeries;
            Barcode = drugBarcode;
            ManufacturerCode = manufacturerCode;
        }
    }
}
