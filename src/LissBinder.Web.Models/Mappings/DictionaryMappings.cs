namespace Escyug.LissBinder.Web.Models.Mappings
{
    internal static class DictionaryMappings
    {
        public static Drugs.DictionaryDrug EntityToModel(Data.Entities.DictionaryDrug dictionaryEntity)
        {
            var drugName = dictionaryEntity.Name;
            var drugformDescription = dictionaryEntity.DrugformDescription;
            var nomenId = dictionaryEntity.NomenId;
            var prepId = dictionaryEntity.PrepId;
            var descriptionId = dictionaryEntity.DescriptionId;
            var drugformId = dictionaryEntity.DrugformId;

            return new Drugs.DictionaryDrug(drugName, drugformDescription,
                nomenId, prepId, descriptionId, drugformId);
        }
    }
}
