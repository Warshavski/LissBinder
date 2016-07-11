using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Mappings
{
    internal static class PharmacyDrugMappings
    {
        public static Models.Drugs.PharmacyDrug EntityToModel(Data.Entities.PharmacyDrug entity)
        {
            var drugCode = entity.Code;
            var drugName = entity.Name;
            var manufactorerCode = entity.ManufacturerCode;
            var manufactorerName = entity.ManufacturerName;

            return new Models.Drugs.PharmacyDrug(
                drugCode, drugName, manufactorerCode, manufactorerName);
        }
    }
}
