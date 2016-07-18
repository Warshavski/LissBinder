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

        event Func<Task> DrugBindAsync;

        string SearchDrugName { get; set; }

        PharmacyDrug SelectedPharmacyDrug { get; }
        DictionaryDrug SelectedDictionaryDrug { get; }

        List<PharmacyDrug> PharmacyDrugs { get; set; }

        IEnumerable<DictionaryDrug> DictionaryDrugs { get; set; }

        bool IsDrugsSearch { set; }

        bool IsDictionarySearch { set; }

        bool IsBinding { set; }

    }
}
