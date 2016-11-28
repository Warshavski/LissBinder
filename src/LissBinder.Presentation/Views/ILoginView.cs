using System;
using System.Threading.Tasks;

using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface ILoginView : IView
    {
        event Func<Task> SignInAsync; 

        string Login { get; set; }
        string Password { get; set; }

        bool IsBusy { get; set; }
    }
}
