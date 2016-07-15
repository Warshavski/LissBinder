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
        event Func<Task> DrugsSearchAsync;

        event Action DrugDetailsShow;

        event Func<Task> DictionarySearchAsync;

        string SearchDrugName { get; set; }

        PharmacyDrug SelectedPharmacyDrug { get; }

        IEnumerable<PharmacyDrug> PharmacyDrugs { get; set; }

        IEnumerable<DictionaryDrug> DictionaryDrugs { get; set; }

        bool IsDrugsSearch { set; }

        bool IsDictionarySearch { set; }

    }
}
