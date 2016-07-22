using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Services;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        private readonly ILoginService _loginService;

        public LoginPresenter(ILoginView view, IApplicationController appController,
            ILoginService loginService) : base(view, appController)
        {
            _loginService = loginService;

            View.LoginExecuteAsync += () => OnLoginExecuteAsync();
        }

        private async Task OnLoginExecuteAsync()
        {
            string login = View.Login.Trim();
            string password = View.Password.Trim();

            if (string.Compare(login, string.Empty) != 0 &&
                string.Compare(password, string.Empty) != 0)
            {
                // call api auth method 
                // create user and auth token
                var token = await _loginService.SignInAsync(login, password);
            }
            else
            {
                View.Error = "Fields can't be empty.";
            }
        }
    }
}
