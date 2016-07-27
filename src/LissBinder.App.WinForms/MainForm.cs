using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.App.WinForms
{
    public partial class MainForm : BaseForm, IMainView
    {
        private readonly ApplicationContext _context;

        public MainForm(ApplicationContext context)
        {
            _context = context;

            InitializeComponent();

            this.buttonSearch.Click += async (sender, e) =>
                await Invoker.InvokeAsync(DrugsSearchAsync);

            this.buttonBind.Click += async (sender, e) =>
                await Invoker.InvokeAsync(DrugBindAsync);

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

            this.splitContainer1.SplitterMoved += (sender, e) => CenterProgressBox();

            this.contextMenuStrip1.Items[0].Click += (sender, e) =>
                Invoker.Invoke(DrugDetailsShow);

            importToolStripMenuItem.Click += (sender, e) =>
                Invoker.Invoke(ImportShow);
        }

      
        //---------------------------------------------------------------------


        #region IView members


        public new void Show()
        {
            _context.MainForm = this;
            base.Show();
            //Application.Run(_context);
        }


        #endregion


        //---------------------------------------------------------------------


        #region IMainView members


        public event Func<Task> DrugsSearchAsync;

        public event Action DrugDetailsShow;

        public event Action ImportShow;

        public event Func<Task> DictionarySearchAsync;

        public event Func<Task> DrugBindAsync;

        public string SearchDrugName
        {
            get
            {
                return textBoxSearch.Text;
            }
            set
            {
                textBoxSearch.Text = value;
            }
        }

        public Models.Drugs.PharmacyDrug SelectedPharmacyDrug
        {
            get
            {
                return this.dataGridViewDrugs.SelectedRows[0].DataBoundItem as Models.Drugs.PharmacyDrug;
            }
        }

        public Models.Drugs.DictionaryDrug SelectedDictionaryDrug 
        {
            get
            {
                return this.dataGridViewDictionary.SelectedRows[0].DataBoundItem as Models.Drugs.DictionaryDrug;
            }
        }

        public List<Escyug.LissBinder.Models.Drugs.PharmacyDrug> PharmacyDrugs
        {
            get
            {
                return dataGridViewDrugs.DataSource as List<Models.Drugs.PharmacyDrug>;
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
                progressBoxDictionary.Visible = value;
                dataGridViewDictionary.Enabled = !value;
            }
        }

        public bool IsDrugsSearch 
        {
            set { progressBoxDrugs.Visible = value; }
        }

        public bool IsBinding { set { if (!value) dataGridViewDrugs.Refresh(); } }


        public string Heading 
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        #endregion IMainView members


        //---------------------------------------------------------------------


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
