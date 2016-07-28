using System;
using System.Linq;
using System.Threading.Tasks;

using Escyug.LissBinder.Models;
using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services;
using Escyug.LissBinder.Models.Services.Exceptions;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;
using Escyug.LissBinder.Presentation.Utils.EventAggregator;
using Escyug.LissBinder.Presentation.Messages;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class MainPresenter : BasePresenter<IMainView, User>
    {
        private User _user;
        private string _lastSearchName;

        private readonly IEventAggregator _eventAggregator;

        private readonly IDictionaryService _dictionaryService;
        private readonly IPharmacyService _pharmacyService;

        public MainPresenter(IMainView view, IApplicationController appController, 
            IDictionaryService dictionaryService,
            IPharmacyService pharmacyService,
            IEventAggregator eventAggregator) : base(view, appController)
        {
            // class globals initialization
            //-------------------------
            _user = null;
            _lastSearchName = string.Empty;
            

            // injected members
            //-------------------------
            _eventAggregator = eventAggregator;
            _dictionaryService = dictionaryService;
            _pharmacyService = pharmacyService;

            
            // events subscription
            //-------------------------
            View.DrugsSearchAsync += () => OnDrugsSearchAsync(View.SearchDrugName);
            View.DictionarySearchAsync += () => OnDictionarySearchAsync(View.SelectedPharmacyDrug);
            View.DrugBind += () => OnDrugBind(View.SelectedPharmacyDrug, View.SelectedDictionaryDrug);
            View.DrugDetailsShow += () => OnDrugDetailsShow(View.SelectedPharmacyDrug);
            View.ImportShow += () => OnImportShow();
        }


        public override void Run(User argument)
        {
            _user = argument;

            View.Heading = _user.UserName;

            View.Show();
        }


        #region Presenter methods

        private async Task OnDrugsSearchAsync(string drugName)
        {
            if (string.Compare(drugName.Trim(), string.Empty) != 0)
            {
                View.IsDrugsSearch = true;
               
                try
                {
                    var pharmacyDrugsList = await _pharmacyService.GetDrugsAsync(drugName);

                    if (pharmacyDrugsList != null)
                    {
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

        private void OnDrugBind(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug)
        {
            if (pharmacyDrug != null && dictionaryDrug != null)
            {
                var bindingSubscription =
                    _eventAggregator.Subscribe<BindingMessage>(OnBindingComplete);

                var binding = new Binding(pharmacyDrug, dictionaryDrug);

                AppController.Run<BindingPresenter, Binding>(binding);
            }
            else
            {
                View.Notify = "Select drug for binding first!";
            }
        }

        private void OnBindingComplete(BindingMessage bindingMessage)
        {
            var pharmacyDrugCode = bindingMessage.Binding.PharmacyDrugCode;

            var newList = View.PharmacyDrugs;
            View.PharmacyDrugs = null;

            newList.RemoveAll(x => x.Code == pharmacyDrugCode);
            View.PharmacyDrugs = newList;
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
        //---------------------------------------------------------------------

    }
}
