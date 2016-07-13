using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        public LoginPresenter(ILoginView view, IApplicationController appController)
            : base(view, appController)
        {
            View.LoginExecute += () => OnLoginExecute();
        }

        private void OnLoginExecute()
        {
            string login = View.Login.Trim();
            string password = View.Password.Trim();

            if (string.Compare(login, string.Empty) != 0 &&
                string.Compare(password, string.Empty) != 0)
            {
                // call api auth method 
                // create user and auth token
            }
            else
            {
                View.Error = "Fields can't be empty.";
            }
        }
    }
}
