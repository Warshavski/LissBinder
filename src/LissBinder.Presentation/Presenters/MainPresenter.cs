using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;
using System.Linq.Expressions;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        // web api url context
        private string _lastSearchName;
        private string _apiUri;

        public MainPresenter(IMainView view, IApplicationController appController)
            : base (view, appController)
        {
            _lastSearchName = string.Empty;
            _apiUri = "http://localhost:49623/";

            View.DrugsSearchAsync += () => OnDrugsSearchAsync(View.SearchDrugName);
            View.DictionarySearchAsync += () => OnDictionarySearchAsync(View.SelectedPharmacyDrug);
            View.DrugDetailsShow += () => OnDrugDetailsShow(View.SelectedPharmacyDrug);
            View.DrugBindAsync += () => OnDrugBindAsync(View.SelectedPharmacyDrug, View.SelectedDictionaryDrug);
        }

        private async Task OnDrugsSearchAsync(string drugName)
        {
            if (String.Compare(drugName.Trim(), string.Empty) != 0)
            {
                View.IsDrugsSearch = true;
               
                var responseAddress = "api/drugs/1/" + drugName;

                var pharmacyDrugsList = 
                    await TryGetEntityAsync<List<PharmacyDrug>>(_apiUri, responseAddress);

                if (pharmacyDrugsList != null)
                {
                    //View.PharmacyDrugs = null;
                    View.PharmacyDrugs = pharmacyDrugsList;
                }
               
                View.IsDrugsSearch = false;
            }
            else
            {
                View.Notify = "Please, enter drug name.";
            }
        }

        private async Task OnDictionarySearchAsync(PharmacyDrug pharmacyDrug)
        {
            var searchName = pharmacyDrug.Name.Split(' ')[0];

            if (_lastSearchName != searchName)
            {
                View.IsDictionarySearch = true;

                var responseAddress = "api/dictionary/" + searchName;
                var dictionaryDrugsList =
                    await TryGetEntityAsync<List<DictionaryDrug>>(_apiUri, responseAddress);

                if (dictionaryDrugsList != null)
                {
                    //View.DictionaryDrugs = null;
                    View.DictionaryDrugs = dictionaryDrugsList;
                }

                _lastSearchName = searchName;

                View.IsDictionarySearch = false;
            }
        }

        private async Task<TEntity> TryGetEntityAsync<TEntity>(string apiUri, string responseAddress)
        {
            try
            {
                var entity = await HttpHelper.GetEntityAsync<TEntity>(_apiUri, responseAddress);
                if (EqualityComparer<TEntity>.Default.Equals(entity, default(TEntity)))
                {
                    View.Notify = "Entity not found.";
                }
                return entity;
            }
            catch (HttpRequestException ex)
            {
                View.Error = ex.Message;
                return default(TEntity);
            }
        }

        private void OnDrugDetailsShow(PharmacyDrug pharmacyDrug)
        {
            AppController.Run<DetailsPresenter, PharmacyDrug>(pharmacyDrug);
        }

        //*** create binding presenter
        private async Task OnDrugBindAsync(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug)
        {
            View.IsBinding = true;

            var pharmacyDrugCode = pharmacyDrug.Code;
            var pharmacyDrugProdCode = pharmacyDrug.ManufacturerCode;
            var descriptionId = dictionaryDrug.DescriptionId;
            var drugformId = dictionaryDrug.DrugformId;
            var nomenId = dictionaryDrug.NomenId;
            var prepId = dictionaryDrug.PrepId;
            var pharmacyId = 1;//model.PharmacyId;

            var binding = new Models.Binding(pharmacyDrugCode, pharmacyDrugProdCode, 
                descriptionId, drugformId, nomenId, prepId, pharmacyId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49623/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var wat = await client.PostAsJsonAsync("api/bind", binding);
                if (wat.IsSuccessStatusCode)
                {
                    //*** create separate method
                    var newList = View.PharmacyDrugs;
                    View.PharmacyDrugs = null;

                    newList.RemoveAll(x => x.Code == pharmacyDrugCode);
                    View.PharmacyDrugs = newList;
                    //***

                    View.Notify = "Binding done";
                    View.IsBinding = false;
                }
                else
                {
                    View.Error = "Some error was occured";
                }
            }
        }

    }
}
