using System.Collections.Generic;
using System.Data;
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

        public static DataTable ModelToDataTable(IEnumerable<Models.Drugs.PharmacyDrug> drugsList, int pharmacyId)
        {
            /**
           * code         - long
           * name         - string
           * producer     - string
           * quantity     - decimal
           * price        - decimal
           * seria        - string
           * barcode      - string
           * prodcode     - int
           * id_pharmacy  - int
           */

            var table = new DataTable();
            table.Columns.Add("code", typeof(long));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("producer", typeof(string));
            table.Columns.Add("quantity", typeof(decimal));
            table.Columns.Add("price", typeof(decimal));
            table.Columns.Add("seria", typeof(string));
            table.Columns.Add("barcode", typeof(string));
            table.Columns.Add("prodcode", typeof(int));
            table.Columns.Add("id_pharmacy", typeof(int));

            foreach (var drug in drugsList)
            {
                table.Rows.Add(
                    drug.Code,
                    drug.Name,
                    drug.ManufacturerName,
                    drug.Quantity,
                    drug.Price,
                    drug.Series,
                    drug.Barcode,
                    drug.ManufacturerCode,
                    pharmacyId
                    );
            }

            return table;
        }
    }
}
