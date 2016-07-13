using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models;
using Escyug.LissBinder.Models.Drugs;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class DictionaryPresenter : BasePresenter<IDictionaryView, PharmacyDrug>
    {
        private PharmacyDrug _pharmacyDrug;

        public DictionaryPresenter(IDictionaryView view, IApplicationController appController)
            : base (view, appController)
        {
            View.InitializeDictionaryAsync += () => OnInitializeDictionaryAsync();
            View.CloseForm += () => OnCloseForm();
        }

        public override void Run(PharmacyDrug argument)
        {
            _pharmacyDrug = argument;
            View.Show();
        }

        private async Task OnInitializeDictionaryAsync()
        {
            View.PharmacyDrugName = 
                _pharmacyDrug.Name + ' ' + _pharmacyDrug.ManufacturerName;

            var searchName = _pharmacyDrug.Name.Split(' ')[0];

            View.IsProgress = true;

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

            View.IsProgress = false;
        }

        private void OnCloseForm()
        {
            View.Close();
        }
    }
}
