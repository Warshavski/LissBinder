using System.Threading.Tasks;

using Escyug.LissBinder.Models.Services;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;
using Escyug.LissBinder.Models;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public sealed class LoginPresenter : BasePresenter<ILoginView>
    {
        private readonly ILoginService _loginService;

        public LoginPresenter(ILoginView view, IApplicationController appController,
            ILoginService loginService) : base(view, appController)
        {
            _loginService = loginService;

            View.SignInAsync += () => OnSignInAsync(View.Login.Trim(), View.Password.Trim());
        }

        private async Task OnSignInAsync(string login, string password)
        {
            if (string.Compare(login, string.Empty) != 0 &&
                string.Compare(password, string.Empty) != 0)
            {
                // call api auth method 
                // create user and auth token
                View.IsBusy = true;

                var user = await _loginService.SignInAsync(login, password);
                if (user != null)
                {
                    AppController.Run<MainPresenter, User>(user);
                    View.Close();
                }
                else
                {
                    View.Notify = "No such user";
                }

                View.IsBusy = false;    
            }
            else
            {
                View.Error = "Fields can't be empty.";
            }
        }
    }
}
