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

            this.buttonSearch.Click += async (sender, e) =>
                await Invoker.InvokeAsync(DrugsSearchAsync);

            this.dataGridViewDrugs.CellClick += async (sender, e) =>
                await Invoker.InvokeAsync(DictionarySearchAsync);

            this.dataGridViewDrugs.RowHeaderMouseClick += async (sender, e) =>
                await Invoker.InvokeAsync(DictionarySearchAsync);

            this.dataGridViewDrugs.MouseClick += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        ContextMenu m = new ContextMenu();

                        int currentMouseOverRow = dataGridViewDrugs.HitTest(e.X, e.Y).RowIndex;

                        if (currentMouseOverRow >= 0)
                        {
                            //contextMenuStrip1.MenuItems.Add(
                            //    new MenuItem(
                            //        string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                            contextMenuStrip1.Show(dataGridViewDrugs, new Point(e.X, e.Y));
                        }
                    }
                };

            this.Resize += (sender, e) => CenterProgressBox();

            this.contextMenuStrip1.Items[0].Click += (sender, e) =>
                Invoker.Invoke(DrugDetailsShow);
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


        public event Func<Task> DrugsSearchAsync;

        public event Action DrugDetailsShow;

        public event Func<Task> DictionarySearchAsync;

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

        public Models.Drugs.PharmacyDrug SelectedPharmacyDrug
        {
            get
            {
                return this.dataGridViewDrugs.SelectedRows[0].DataBoundItem as Models.Drugs.PharmacyDrug;
            }
        }


        public IEnumerable<Escyug.LissBinder.Models.Drugs.PharmacyDrug> PharmacyDrugs
        {
            get
            {
                return dataGridViewDrugs.DataSource as IEnumerable<Models.Drugs.PharmacyDrug>;
            }
            set
            {
                dataGridViewDrugs.DataSource = value;
            }
        }

        public IEnumerable<Models.Drugs.DictionaryDrug> DictionaryDrugs
        {
            get
            {
                return dataGridViewDictionary.DataSource as IEnumerable<Models.Drugs.DictionaryDrug>;
            }
            set
            {
                dataGridViewDictionary.DataSource = value;
            }
        }

        public bool IsDictionarySearch
        {
            set 
            {
                dataGridViewDrugs.Enabled = !value;
                progressBoxDictionary.Visible = value; 
            }
        }

        public bool IsDrugsSearch 
        {
            set { progressBoxDrugs.Visible = value; }
        }

        #endregion IMainView members


        //-------------------------------------------------


        #region Helper methods

        //*** not so good
        private void CenterProgressBox()
        {
            CenterProgressBox(progressBoxDictionary, dataGridViewDictionary);
            CenterProgressBox(progressBoxDrugs, dataGridViewDrugs);
        }

        /// <summary>
        /// Center the progress box relative to the container.
        /// </summary>
        /// <param name="progressBox">UI Control that represent progress box.</param>
        /// <param name="container">UI Control that contains progress box.</param>
        private void CenterProgressBox(Control progressBox, Control container)
        {
            var progressPosX = (container.Width / 2) - (progressBox.Width / 2);
            var progressPosY = (container.Height / 2) - (progressBox.Height / 2);

            progressBox.Location =
                new System.Drawing.Point(progressPosX, progressPosY);
        }

        #endregion Helper methods 
    
    }
}
