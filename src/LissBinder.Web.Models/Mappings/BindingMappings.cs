
namespace Escyug.LissBinder.Web.Models.Mappings
{
    public static class BindingMappings
    {
        public static Data.Entities.Binding ModelToEntity(Models.Binding binding)
        {
            var entity = new Data.Entities.Binding();

            entity.PharmacyDrugCode = binding.PharmacyDrugCode;
            entity.PharmacyDrugProdCode = binding.PharmacyDrugProdCode;
            entity.DescriptionId = binding.DescriptionId;
            entity.DrugformId = binding.DrugformId;
            entity.NomenId = binding.NomenId;
            entity.PrepId = binding.PrepId;
            entity.PharmacyId = binding.PharmacyId;

            return entity;
        }
    }
}
