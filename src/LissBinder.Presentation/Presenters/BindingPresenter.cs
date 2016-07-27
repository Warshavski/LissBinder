using System.Threading.Tasks;

using Escyug.LissBinder.Models;
using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services;
using Escyug.LissBinder.Models.Services.Exceptions;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;
using Escyug.LissBinder.Presentation.Messages;
using Escyug.LissBinder.Presentation.Utils.EventAggregator;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class BindingPresenter : BasePresenter<IBindingView, Binding>
    {
        private Binding _binding;
        
        private readonly IEventAggregator _eventAggregator;
        private readonly IBindingService _bindingService;

        public BindingPresenter(IBindingView view, IApplicationController appController,
            IBindingService bindingService,
            IEventAggregator eventAggregator) : base(view, appController)
        {
            // class globals initialization
            //-------------------------
            _binding = null;


            // injection members
            //-------------------------
            _bindingService = bindingService;
            _eventAggregator = eventAggregator;

            View.BindingInitializeAsync += () => OnBindingInitializeAsync(_binding);
        }

        public override void Run(Binding argument)
        {
            _binding = argument;

            View.Show();
        }

        private async Task OnBindingInitializeAsync(Binding binding)
        {
            View.IsBusy = true;

            try
            {
                var isSucceeded = await _bindingService.BindAsync(binding);
                if (isSucceeded)
                {
                    // call event aggregator
                    var message = new BindingMessage(binding);
                    _eventAggregator.Publish<BindingMessage>(message);
                }
            }
            catch (ServiceException ex)
            {
                View.Error = ex.Message;
            }
            finally 
            {
                View.IsBusy = false;
                View.Close();
            }
        }
    }
}
