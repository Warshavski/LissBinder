namespace Escyug.LissBinder.Web.Models.Mappings
{
    internal static class PharmacyDrugMappings
    {
        public static Models.Drugs.PharmacyDrug EntityToModel(Data.Entities.PharmacyDrug drugEntity)
        {
            var drugCode = drugEntity.Code;
            var drugName = drugEntity.Name;
            var manufacturerName = drugEntity.ManufacturerName;
            var drugQuantity = drugEntity.Quantity;
            var drugPrice = drugEntity.Price;
            var drugSeries = drugEntity.Series;
            var drugBarcode = drugEntity.Barcode;
            var manufacturerCode = drugEntity.ManufacturerCode;

            return new Drugs.PharmacyDrug(drugCode, drugName, manufacturerName,
                drugQuantity, drugPrice, drugSeries, drugBarcode, manufacturerCode);
        }

        public static Data.Entities.PharmacyDrug ModelToEntity(Models.Drugs.PharmacyDrug drugModel)
        {
            var drugEntity = new Data.Entities.PharmacyDrug();

            drugEntity.Code = drugModel.Code;
            drugEntity.Name = drugModel.Name;
            drugEntity.ManufacturerName = drugModel.ManufacturerName;
            drugEntity.Quantity = drugModel.Quantity;
            drugEntity.Price = drugModel.Price;
            drugEntity.Series = drugModel.Series;
            drugEntity.Barcode = drugModel.Barcode;
            drugEntity.ManufacturerCode = drugModel.ManufacturerCode;

            return drugEntity;
        }
    }
}
