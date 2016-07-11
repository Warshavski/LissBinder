
namespace Escyug.LissBinder.Models.Drugs
{
    public class PharmacyDrug
    {
        public long Code { get; private set; }

        public string Name { get; private set; }

        public int ManufacturerCode { get; private set; }

        public string ManufacturerName { get; private set; }

        public PharmacyDrug(long drugCode, string drugName,
            int manufacturerCode, string manufacturerName)
        {
            Code = drugCode;
            Name = drugName;
            ManufacturerCode = manufacturerCode;
            ManufacturerName = manufacturerName;
        }
    }
}
