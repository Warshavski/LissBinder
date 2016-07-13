using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IDetailsView : IView
    {
        event Action CloseForm;

        string DrugName { set; }
        string Manufacturer { set; }
        string Series { set; }
        string Quantity { set; }
        string Price { set; }
        string Barcode { set; }
    }
}
