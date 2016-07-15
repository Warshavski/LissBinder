
namespace Escyug.LissBinder.Models.Mappings
{
    internal static class BindingMappings
    {
        public static Data.Entities.Binding ModelToEntity(Models.Binding model)
        {
            var entity = new Data.Entities.Binding();

            entity.PharmacyDrugCode = model.PharmacyDrugCode;
            entity.PharmacyDrugProdCode = model.PharmacyDrugProdCode;
            entity.DescriptionId = model.DescriptionId;
            entity.DrugformId = model.DrugformId;
            entity.NomenId = model.NomenId;
            entity.PrepId = model.PrepId;
            entity.PharmacyId = model.PharmacyId;

            return entity;
        }
    }
}
