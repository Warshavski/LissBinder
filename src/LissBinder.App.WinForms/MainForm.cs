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
    public partial class MainForm : Form, IMainView
    {
        private readonly ApplicationContext _context;

        public MainForm(ApplicationContext context)
        {
            _context = context;

            InitializeComponent();

            this.button1.Click += async (sender, e) => 
                await Invoker.InvokeAsync(SearchDrugsAsync);
        }

        #region IView members

        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(_context);
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


        #region IMainView members


        public event Func<Task> SearchDrugsAsync;

        public string SearchDrugName
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public IEnumerable<Escyug.LissBinder.Models.Drugs.PharmacyDrug> PharmacyDrugs
        {
            get
            {
                return dataGridView1.DataSource as IEnumerable<Models.Drugs.PharmacyDrug>;
            }
            set
            {
                dataGridView1.DataSource = value;
            }
        }


        #endregion IMainView members

    }
}
