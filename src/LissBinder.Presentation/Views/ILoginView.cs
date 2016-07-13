using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface ILoginView : IView
    {
        event Action LoginExecute; 

        string Login { get; set; }
        string Password { get; set; }
    }
}
