using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Escyug.LissBinder.Models.Services;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;
using Escyug.LissBinder.Presentation.Presenters;
using Escyug.LissBinder.Presentation.Utils.EventAggregator;
using Escyug.LissBinder.Models.Services.Common;

namespace Escyug.LissBinder.App.WinForms
{
    static class Program
    {
        public static readonly ApplicationContext Context = new ApplicationContext();

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LoginForm());

            var apiUri = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
            
            var controller = new ApplicationController(new LightInjectAdapter()).
                RegisterInstance<IEventAggregator>(new SimpleEventAggregator()).
                RegisterInstance(new ApplicationContext());

            ConfigureViews(controller);
            ConfigureApiContext(controller);
            ConfigureServices(controller);

            controller.Run<LoginPresenter>();
        }

        private static void ConfigureApiContext(IApplicationController controller)
        {
            var apiUri = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
            
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
                .RegisterService<IImportService, ImportService>()
                .RegisterService<IBindingService, BindingService>();
        }
    }
}
