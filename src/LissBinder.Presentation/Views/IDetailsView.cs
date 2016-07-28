using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IDetailsView : IView
    {
        string DrugName { set; }
        string Manufacturer { set; }
        string Series { set; }
        string Quantity { set; }
        string Price { set; }
        string Barcode { set; }
    }
}
