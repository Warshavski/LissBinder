using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Data;
using Escyug.LissBinder.Data.SqlServer.QueryProcessors;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Repositories;

namespace Escyug.LissBinder.Console.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DbContext("Data Source=localhost;Initial Catalog=lissMerged;Integrated Security=True;");
            
            var dictionaryQueryProcessor = new DictionaryDrugsByNameQueryProcessor(context);
            var dictionaryRepository = new DictionaryDrugsRepository(dictionaryQueryProcessor);
            wat(dictionaryRepository).Wait();

            PrintDelimeter();

            var pharmacyDrugQueryProcessor = new PharmacyDrugsByNameQueryProcessor(context);
            var pharmacyDrugRepository = new PharmacyDrugsRepository(pharmacyDrugQueryProcessor);
            wat(pharmacyDrugRepository).Wait();
        }

        // DICTIONARY SECTION
        //------------------------------------------------------------

        public async static Task wat(IDictionaryDrugsRepository repo)
        {
            var drugs = await repo.GetDrugsByNameAsync("фено");
            PrintDrugs(drugs);
        }

        public static void PrintDrugs(IEnumerable<DictionaryDrug> drugsList)
        {
            foreach (var drug in drugsList)
            {
                System.Console.WriteLine(drug.Name);
                System.Console.WriteLine(drug.DrugformDescription);
                System.Console.WriteLine("-----------------------------");
            }
        }

        // PHARMACY DRUG SECTION
        //------------------------------------------------------------

        public async static Task wat(IPharmacyDrugsRepository repo)
        {
            var drugs = await repo.GetDrugsByNameAsync("фено", 1);
            PrintDrugs(drugs);
        }

        public static void PrintDrugs(IEnumerable<PharmacyDrug> drugsList)
        {
            foreach (var drug in drugsList)
            {
                System.Console.WriteLine(drug.Name);
                System.Console.WriteLine(drug.ManufacturerName);
                System.Console.WriteLine("-----------------------------");
            }
        }


        private static void PrintDelimeter()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("******** ********");
            System.Console.WriteLine("******** ********");
            System.Console.WriteLine();
        }
    }
}
