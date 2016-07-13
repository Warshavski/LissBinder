using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IMainView : IView
    {
        event Func<Task> SearchDrugsAsync;

        event Action OpenDictionary;

        event Action ShowDrugDetails;

        string SearchDrugName { get; set; }

        IEnumerable<PharmacyDrug> PharmacyDrugs { get; set; }

        PharmacyDrug SelectedPharmacyDrug { get; }

        bool IsProgress { set; }
    }
}
