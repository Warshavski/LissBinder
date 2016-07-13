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

        string SearchDrugName { get; set; }

        IEnumerable<PharmacyDrug> PharmacyDrugs { get; set; }
    }
}
