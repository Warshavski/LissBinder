using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Presentation.Common;

using Escyug.LissBinder.Models.Metadata;

namespace Escyug.LissBinder.Presentation.Views
{
    public interface IImportView : IView
    {
        event Action ViewInitialize;
        event Action ShowColumnsMetadata;

        event Func<Task> ImportExecuteAsync;
        event Func<Task> MetadataLoadAsync;
        
        string ConnectionString { get; }

        IEnumerable<TableMetadata> TablesMetadata { get; set; }

        TableMetadata SelectedTableMetadata { get; }

        IEnumerable<ColumnMetadata> SelectedColumnsMetadata { get; set; }

        IEnumerable<string> DestinationColumnsNames { get; set; }
    }
}