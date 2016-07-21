using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services;
using Escyug.LissBinder.Models.Services.Exceptions;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private string _lastSearchName;

        private readonly IDictionaryService _dictionaryService;
        private readonly IPharmacyService _pharmacyService;

        public MainPresenter(IMainView view, IApplicationController appController, 
            IDictionaryService dictionaryService,
            IPharmacyService pharmacyService) : base(view, appController)
        {
            _lastSearchName = string.Empty;

            
            // injected members
            //-------------------------
            _dictionaryService = dictionaryService;
            _pharmacyService = pharmacyService;
            
            // events subscription
            //-------------------------
            View.DrugsSearchAsync += () => OnDrugsSearchAsync(View.SearchDrugName);
            View.DictionarySearchAsync += () => OnDictionarySearchAsync(View.SelectedPharmacyDrug);
            View.DrugBindAsync += () => OnDrugBindAsync(View.SelectedPharmacyDrug, View.SelectedDictionaryDrug);
            View.DrugDetailsShow += () => OnDrugDetailsShow(View.SelectedPharmacyDrug);
            View.ImportShow += () => OnImportShow();
        }


        //---------------------------------------------------------------------


        #region Presenter methods

        private async Task OnDrugsSearchAsync(string drugName)
        {
            if (String.Compare(drugName.Trim(), string.Empty) != 0)
            {
                View.IsDrugsSearch = true;
               
                try
                {
                    var pharmacyDrugsList = await _pharmacyService.GetDrugsAsync(drugName);

                    if (pharmacyDrugsList != null)
                    {
                        //View.PharmacyDrugs = null;
                        View.PharmacyDrugs = pharmacyDrugsList.ToList();
                    }
                    else
                    {
                        View.Notify = "Drug not found";
                    }
                }
                catch (ServiceException ex)
                {
                    View.Error = ex.Message;
                }
                finally
                {
                    View.IsDrugsSearch = false;
                }
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

                try
                {
                    var dictionaryDrugsList = await _dictionaryService.GetDrugsAsync(searchName);
                    
                    if (dictionaryDrugsList != null)
                    {
                        //View.DictionaryDrugs = null;
                        View.DictionaryDrugs = dictionaryDrugsList;

                        _lastSearchName = searchName;
                    }
                }
                catch (ServiceException ex)
                {
                    View.Error = ex.Message;
                }
                finally
                {
                    View.IsDictionarySearch = false;
                }
            }
        }

       

        //*** create binding presenter
        private async Task OnDrugBindAsync(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug)
        {
            View.IsBinding = true;

            try
            {
                var isBindSuccessful =
                    await _pharmacyService.BindPharmacyDrugAsync(pharmacyDrug, dictionaryDrug, 1);

                if (isBindSuccessful)
                {
                    View.Notify = "Binding done";

                    //*** create separate method
                    var newList = View.PharmacyDrugs;
                    View.PharmacyDrugs = null;

                    newList.RemoveAll(x => x.Code == pharmacyDrug.Code);
                    View.PharmacyDrugs = newList;
                    //***  
                }
            }

            catch (ServiceException ex)
            {
                View.Error = ex.Message;
            }
            finally
            {
                View.IsBinding = false;
            }
        }


        private void OnDrugDetailsShow(PharmacyDrug pharmacyDrug)
        {
            AppController.Run<DetailsPresenter, PharmacyDrug>(pharmacyDrug);
        }

        private void OnImportShow()
        {
            AppController.Run<ImportPresenter>();
        }

        #endregion Presenter methods

    }
}
