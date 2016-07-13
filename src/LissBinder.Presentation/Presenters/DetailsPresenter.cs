
using Escyug.LissBinder.Models.Drugs;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public class DetailsPresenter : BasePresenter<IDetailsView, PharmacyDrug>
    {
        public DetailsPresenter(IDetailsView view, IApplicationController appController)
            : base (view, appController)
        {
            View.CloseForm += () => OnCloseForm();
        }

        public override void Run(PharmacyDrug argument)
        {
            View.Show();
            ShowDetails(argument);
        }

        private void ShowDetails(PharmacyDrug drug)
        {
            View.DrugName = drug.Name;
            View.Manufacturer = drug.ManufacturerName;
            View.Series = drug.Series;
            View.Quantity = drug.Quantity.ToString();
            View.Price = string.Format("{0:C}", drug.Price);
            View.Barcode = drug.Barcode;
        }

        private void OnCloseForm()
        {
            View.Close();
        }
    }
}
