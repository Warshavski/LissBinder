﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

namespace Escyug.LissBinder.App.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task.Run(TestHttpClient).Wait();
            //Task.WaitAll(TestHttpClient);
            TestHttpClient().Wait();
        }

        private static async Task TestHttpClient()
        {
            using (var client = new HttpClient())
            {
                // HttpClient setup
                client.BaseAddress = new Uri("http://localhost:49623/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // cal web api controller method
                HttpResponseMessage response = await client.GetAsync("api/dictionary/фенотропил");
                if (response.IsSuccessStatusCode)
                {
                    var drugs = await response.Content.ReadAsAsync<IEnumerable<DictionaryDrug>>();
                    foreach (var drug in drugs)
                    {
                        System.Console.WriteLine("{0} - {1}\n\r{2}", 
                            drug.Name, drug.DrugformDescription, "-----------------------");
                    }
                }
            }
        }
    }
}
