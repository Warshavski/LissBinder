using System;
using System.Configuration;
using System.Windows.Forms;

using Escyug.LissBinder.Models.Services;
using Escyug.LissBinder.Models.Services.Common;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Presenters;
using Escyug.LissBinder.Presentation.Utils.EventAggregator;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.App.WinForms
{
    internal static class Program
    {
        internal static readonly ApplicationContext Context = new ApplicationContext();

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var controller = new ApplicationController(new LightInjectAdapter())
                .RegisterInstance<IEventAggregator>(new SimpleEventAggregator())
                .RegisterInstance(new ApplicationContext());

            ConfigureViews(controller);
            ConfigureApiContext(controller);
            ConfigureServices(controller);

            controller.Run<LoginPresenter>();
        }

        private static void ConfigureApiContext(IApplicationController controller)
        {
            #if DEBUG
            var apiUri = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
            #endif
            
            controller.RegisterInstance(new ApiContext(apiUri));
        }

        private static void ConfigureViews(IApplicationController controller) 
        {
            controller.RegisterView<ILoginView, LoginForm>()
                .RegisterView<IMainView, MainForm>()
                .RegisterView<IImportView, ImportForm>()
                .RegisterView<IDetailsView, DetailsForm>()
                .RegisterView<IBindingView, BindingForm>();
        }

        private static void ConfigureServices(IApplicationController controller)
        {
            controller.RegisterService<ILoginService, LoginService>()
                .RegisterService<IDictionaryService, DictionaryService>()
                .RegisterService<IPharmacyService, PharmacyService>()
                .RegisterService<IDataImportService, DataImportService>()
                .RegisterService<IBindingService, BindingService>();
        }
    }
}
