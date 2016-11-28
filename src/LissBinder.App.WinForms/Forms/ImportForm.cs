using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.App.WinForms
{
    public partial class ImportForm : BaseForm, IImportView
    {
        public ImportForm()
        {
            InitializeComponent();

            this.Load += (sender, e) => Invoker.Invoke(ViewInitialize);

            this.button1.Click += async (sender, e) => await Invoker.InvokeAsync(MetadataLoadAsync);
            this.button2.Click += async (sender, e) => await Invoker.InvokeAsync(ImportExecuteAsync);

            this.comboBox1.SelectedValueChanged += (sender, e) => Invoker.Invoke(ShowColumnsMetadata);
        }



        //---------------------------------------------------------------------


        #region IView members 


        public new void Show()
        {
            ShowDialog();
        }


        #endregion



        //---------------------------------------------------------------------


        #region IImportView members


        public event Action ViewInitialize;

        public event Action ShowColumnsMetadata;

        public event Func<Task> ImportExecuteAsync;

        public event Func<Task> MetadataLoadAsync;

        public string ConnectionString
        {
            get 
            {
                var connectionStringBuilder = new StringBuilder();
                connectionStringBuilder.Append("Provider=Microsoft.Jet.OLEDB.4.0;");
                connectionStringBuilder.Append(string.Format("Data Source={0};",textBox1.Text.Trim()));
                connectionStringBuilder.Append("User ID=Admin;Password=;");
                connectionStringBuilder.Append("Extended Properties='dBASE IV';");

                return connectionStringBuilder.ToString();
            }
        }

        public IEnumerable<Models.Metadata.TableMetadata> TablesMetadata
        {
            get
            {
                return comboBox1.DataSource as IEnumerable<Models.Metadata.TableMetadata>;
            }
            set
            {
                comboBox1.DataSource = value;
                comboBox1.DisplayMember = "Name";
            }
        }

        public Models.Metadata.TableMetadata SelectedTableMetadata
        {
            get
            {
                return comboBox1.SelectedValue as Models.Metadata.TableMetadata;
            }
        }

        public IEnumerable<Models.Metadata.ColumnMetadata> SelectedColumnsMetadata
        {
            get
            {
                return dataGridView1.DataSource as IEnumerable<Models.Metadata.ColumnMetadata>;
            }
            set
            {
                dataGridView1.DataSource = value;
            }
        }

        public IEnumerable<string> DestinationColumnsNames
        {
            get
            {
                return destinationColumn.DataSource as IEnumerable<string>;
            }
            set
            {
                destinationColumn.DataSource = value;
            }
        }


        #endregion IImportView members

    }
}
