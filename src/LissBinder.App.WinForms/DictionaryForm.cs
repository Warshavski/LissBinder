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
    public partial class DictionaryForm : Form, IDictionaryView
    {

        public DictionaryForm()
        {
            InitializeComponent();

            this.Load += async (sender, e) => 
                await Invoker.InvokeAsync(InitializeDictionaryAsync);

            this.buttonClose.Click += (sender, e) => Invoker.Invoke(CloseForm);
        }


        //-------------------------------------------------


        #region IView members

        public new void Show()
        {
            ShowDialog();
        }

        public string Error
        {
            set
            {
                MessageBox.Show(value, "Application error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Notify
        {
            set
            {
                MessageBox.Show(value, "Application information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion IView members


        //-------------------------------------------------


        #region IDictionaryView members


        public event Func<Task> InitializeDictionaryAsync;

        public event Action CloseForm;

        public string PharmacyDrugName
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        public IEnumerable<Models.Drugs.DictionaryDrug> DictionaryDrugs
        {
            get
            {
                return this.dataGridView1.DataSource as IEnumerable<Models.Drugs.DictionaryDrug>;
            }
            set
            {
                this.dataGridView1.DataSource = value;
            }
        }


        public bool IsProgress
        {
            set 
            {
                this.progressBox.Visible = value;
            }
        }

        #endregion IDictionaryView members



        
    }
}
