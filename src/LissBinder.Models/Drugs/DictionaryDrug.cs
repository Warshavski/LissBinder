﻿
namespace Escyug.LissBinder.Models.Drugs
{
    public class DictionaryDrug
    {
        public string Name { get; private set; }

        public string DrugformDescription { get; private set; }

        public int NomenId { get; private set; }

        public int PrepId { get; private set; }

        public int DescriptionId { get; private set; }

        public int DrugformId { get; private set; }

        public DictionaryDrug(string drugName, string drugformDescription, 
            int nomenId, int prepId, int descId, int drugformId)
        {
            Name = drugName;
            DrugformDescription = drugformDescription;
            NomenId = nomenId;
            PrepId = prepId;
            DescriptionId = descId;
            DrugformId = drugformId;
        }
    }
}
