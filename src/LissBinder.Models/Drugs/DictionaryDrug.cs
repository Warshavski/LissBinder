using System;

namespace Escyug.LissBinder.Models.Drugs
{
    //[Serializable]
    public class DictionaryDrug
    {
        public string Name { get; private set; }

        public string DrugformDescription { get; private set; }

        public int NomenId { get; private set; }

        public int PrepId { get; private set; }

        public int DescriptionId { get; private set; }

        public int DrugformId { get; private set; }

        public DictionaryDrug(string name, string drugformDescription,
            int nomenId, int prepId, int descriptionId, int drugformId)
        {
            Name = name;
            DrugformDescription = drugformDescription;
            NomenId = nomenId;
            PrepId = prepId;
            DescriptionId = descriptionId;
            DrugformId = drugformId;
        }
    }
}
