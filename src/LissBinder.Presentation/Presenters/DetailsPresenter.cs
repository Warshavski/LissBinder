
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
            View.CloseForm += () => OnFormClose();
        }

        public override void Run(PharmacyDrug argument)
        {
            ShowDetails(argument);
            View.Show();
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

        private void OnFormClose()
        {
            View.Close();
        }
    }
}
