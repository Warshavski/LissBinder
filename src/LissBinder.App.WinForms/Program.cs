using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


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

            var controller = new ApplicationController(new LightInjectAdapter())
               .RegisterView<IMainView, MainForm>()
               //.RegisterView<IDictionaryView, DictionaryForm>()
               .RegisterView<IDetailsView, DetailsForm>()
               .RegisterInstance(new ApplicationContext());

            controller.Run<MainPresenter>();
        }
    }
}
