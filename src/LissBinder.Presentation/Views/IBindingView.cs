using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IBindingView : IView
    {
        event Func<Task> BindingInitializeAsync;
        bool IsBusy { get; set; }
    }
}
