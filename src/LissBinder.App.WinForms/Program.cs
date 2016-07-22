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
                RegisterView<ILoginView, LoginForm>().
                RegisterView<IMainView, MainForm>().
                RegisterView<IImportView, ImportForm>().
                RegisterView<IDetailsView, DetailsForm>().
                RegisterInstance<ILoginService>(new LoginService(apiUri)).
                RegisterInstance<IDictionaryService>(new DictionaryService(apiUri)).
                RegisterInstance<IPharmacyService>(new PharmacyService(apiUri)).
                RegisterInstance<IImportService>(new ImportService(apiUri)).
                RegisterInstance(new ApplicationContext());

            controller.Run<LoginPresenter>();
        }
    }
}
