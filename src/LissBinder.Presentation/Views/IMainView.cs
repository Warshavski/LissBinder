using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IMainView : IView
    {
        event Func<Task> DrugsSearchAsync;
        event Func<Task> DictionarySearchAsync;
        
        event Action DrugBind;
        event Action DrugDetailsShow;
        event Action ImportShow;

        string SearchDrugName { get; set; }

        PharmacyDrug SelectedPharmacyDrug { get; }
        DictionaryDrug SelectedDictionaryDrug { get; }

        List<PharmacyDrug> PharmacyDrugs { get; set; }

        IEnumerable<DictionaryDrug> DictionaryDrugs { get; set; }

        bool IsDrugsSearch { set; }

        bool IsDictionarySearch { set; }

        string Heading { get; set; }
    }
}
