
namespace Escyug.LissBinder.Models.Mappings
{
    internal static class DictionaryDrugMappings
    {
        public static Models.Drugs.DictionaryDrug EntityToModel(Data.Entities.DictionaryDrug entity)
        {
            var drugName = entity.Name;
            var drugformDescription = entity.DrugformDescription;
            var nomenId = entity.NomenId;
            var prepId = entity.PrepId;
            var descId = entity.DescriptionId;
            var drugformId = entity.DrugformId;

            return new Models.Drugs.DictionaryDrug(drugName, drugformDescription, nomenId,
                prepId, descId, drugformId);
        }
    }
}
