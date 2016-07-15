using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        // web api url context
        private string _lastSearchName;

        public MainPresenter(IMainView view, IApplicationController appController)
            : base (view, appController)
        {
            _lastSearchName = string.Empty;
            View.DrugsSearchAsync += () => OnDrugsSearchAsync(View.SearchDrugName);
            View.DictionarySearchAsync += () => OnDictionarySearch(View.SelectedPharmacyDrug);
            View.DrugDetailsShow += () => OnDrugDetailsShow(View.SelectedPharmacyDrug);
        }

        private async Task OnDrugsSearchAsync(string drugName)
        {
            if (String.Compare(drugName.Trim(), string.Empty) != 0)
            {
                View.IsDrugsSearch = true;

                // async wep api method call
                using (var client = new HttpClient())
                {
                    // HttpClient setup
                    client.BaseAddress = new Uri("http://localhost:49623/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // cal web api controller method
                    var responseAddress = "api/drugs/1/" + drugName;
                    var response = await client.GetAsync(responseAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        var drugs = await response.Content.ReadAsAsync<IEnumerable<PharmacyDrug>>();
                        View.PharmacyDrugs = drugs;
                    }
                    else
                    {
                        View.Notify = "No avaliable result(Drug not found).";
                    }
                }

                View.IsDrugsSearch = false;
            }
            else
            {
                View.Notify = "Please, enter drug name.";
            }
        }

        private async Task OnDictionarySearch(PharmacyDrug pharmacyDrug)
        {
            var searchName = pharmacyDrug.Name.Split(' ')[0];

            if (_lastSearchName != searchName)
            {
                View.IsDictionarySearch = true;
                // async wep api method call
                using (var client = new HttpClient())
                {
                    // HttpClient setup
                    client.BaseAddress = new Uri("http://localhost:49623/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // cal web api controller method
                    var responseAddress = "api/dictionary/" + searchName;
                    var response = await client.GetAsync(responseAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        var drugs = await response.Content.ReadAsAsync<IEnumerable<DictionaryDrug>>();
                        View.DictionaryDrugs = drugs;
                    }
                    else
                    {
                        View.Notify = "No avaliable result(Drug not found).";
                    }
                }
                View.IsDictionarySearch = false;
                _lastSearchName = searchName;
            }
        }

        private void OnDrugDetailsShow(PharmacyDrug pharmacyDrug)
        {
            AppController.Run<DetailsPresenter, PharmacyDrug>(pharmacyDrug);
        }
    }
}
