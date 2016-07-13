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

        public MainPresenter(IMainView view, IApplicationController appController)
            : base (view, appController)
        {
            View.SearchDrugsAsync += () => OnSearchDrugsAsync();
        }

        private async Task OnSearchDrugsAsync()
        {
            var drugName = View.SearchDrugName.Trim();

            if (String.Compare(drugName, string.Empty) != 0)
            {
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
            }
            else
            {
                View.Notify = "Please, enter drug name.";
            }
        }
    }
}
