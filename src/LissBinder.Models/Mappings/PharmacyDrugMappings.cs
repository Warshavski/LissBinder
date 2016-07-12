
namespace Escyug.LissBinder.Models.Mappings
{
    internal static class PharmacyDrugMappings
    {
        public static Models.Drugs.PharmacyDrug EntityToModel(Data.Entities.PharmacyDrug entity)
        {
            var drugCode = entity.Code;
            var drugName = entity.Name;
            var manufactorerName = entity.ManufacturerName;
            var drugQnt = entity.Quantity;
            var drugPrice = entity.Price;
            var drugSeries = entity.Series;
            var drugBarcode = entity.Barcode;
            var manufactorerCode = entity.ManufacturerCode;

            return new Models.Drugs.PharmacyDrug(
                drugCode, drugName, manufactorerName, drugQnt, 
                drugPrice, drugSeries, drugBarcode, manufactorerCode);
        }

        public static Data.Entities.PharmacyDrug ModelToEntity(Models.Drugs.PharmacyDrug model)
        {
            var entity = new Data.Entities.PharmacyDrug();

            entity.Code = model.Code;
            entity.Name = model.Name;
            entity.ManufacturerName = model.ManufacturerName;
            entity.Quantity = model.Quantity;
            entity.Price = model.Price;
            entity.Series = model.Series;
            entity.Barcode = model.Barcode;
            entity.ManufacturerCode = model.ManufacturerCode;

            return entity;
        }
    }
}
