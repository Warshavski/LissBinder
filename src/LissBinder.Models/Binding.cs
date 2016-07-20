
using Escyug.LissBinder.Models.Drugs;
namespace Escyug.LissBinder.Models
{
    public sealed class Binding
    {
        public long PharmacyDrugCode { get; private set; }

        public int PharmacyDrugProdCode { get; private set; }

        public int DescriptionId { get; private set; }

        public int DrugformId { get; private set; }

        public int NomenId { get; private set; }

        public int PrepId { get; private set; }

        public int PharmacyId { get; private set; }

        public Binding(long pharmacyDrugCode, int pharmacyDrugProdCode, 
            int descriptionId, int drugformId, int nomenId, int prepId, int pharmacyId)
        {
            PharmacyDrugCode = pharmacyDrugCode;
            PharmacyDrugProdCode = pharmacyDrugProdCode;
            DescriptionId = descriptionId;
            DrugformId = drugformId;
            NomenId = nomenId;
            PrepId = prepId;
            PharmacyId = pharmacyId;
        }

        //*** use call of the constructor
        public Binding(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug, int pharmacyId)
        {
            PharmacyDrugCode = pharmacyDrug.Code;
            PharmacyDrugProdCode = pharmacyDrug.ManufacturerCode;
            DescriptionId = dictionaryDrug.DescriptionId;
            DrugformId = dictionaryDrug.DrugformId;
            NomenId = dictionaryDrug.NomenId;
            PrepId = dictionaryDrug.PrepId;
            PharmacyId = pharmacyId;
        }
    }
}
