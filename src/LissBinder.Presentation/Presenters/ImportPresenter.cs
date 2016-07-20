using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Metadata;
using Escyug.LissBinder.Models.Services;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.Presentation.Presenters
{
    public sealed class ImportPresenter : BasePresenter<IImportView>
    {
        private readonly IImportService _importService;

        public ImportPresenter(IImportView view, IApplicationController appController,
            IImportService importService) : base(view, appController)
        {
            _importService = importService;

            View.ViewInitialize += () => OnViewInitialize();

            View.MetadataLoadAsync += () => 
                OnMetadataLoadAsync(View.ConnectionString);

            View.ImportExecuteAsync += () =>
                OnImportExecuteAsync(View.ConnectionString, View.SelectedTableMetadata);

            View.ShowColumnsMetadata += () => OnShowColumnsMetadata(View.SelectedTableMetadata);
        }


        // IImport view events subscription methods
        //---------------------------------------------------------------------

        private void OnViewInitialize()
        {
            var destinationColumns = new string[]
            {
                "code", 
                "name", 
                "producer", 
                "quantity", 
                "price", 
                "seria", 
                "barcode", 
                "prodcode"
            };

            View.DestinationColumnsNames = destinationColumns;
        }

        private async Task OnMetadataLoadAsync(string connectionString)
        {
            var metadata = await _importService.GetFileMetadataAsync(connectionString);
            if (metadata != null)
            {
                View.TablesMetadata = metadata;
            }
            else
            {
                View.Error = "Source file load error.";
            }
        }

        private async Task OnImportExecuteAsync(string connectionString, TableMetadata table)
        {
            var operationResult = await _importService.ImportAsync(connectionString, table.Name, 1);
            if (operationResult)
            {
                View.Notify = "Import complete.";
            }
            else
            {
                View.Error = "Error.";
            }
        }

        private void OnShowColumnsMetadata(TableMetadata selectedTableMetadata)
        {
            View.SelectedColumnsMetadata = selectedTableMetadata.Columns;
        }


        // Helper methods
        //---------------------------------------------------------------------

        
    }
}
