using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Presentation.Common;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IDictionaryView : IView
    {
        event Func<Task> InitializeDictionaryAsync;
        event Action CloseForm;

        string PharmacyDrugName { get; set; }

        IEnumerable<DictionaryDrug> DictionaryDrugs { get; set; }

        bool IsProgress { set; }
    }
}
