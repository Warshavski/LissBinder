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
                await Invoker.InvokeAsync(SearchDrugsAsync);

            this.buttonDictionaryOpen.Click += (sender, e) => Invoker.Invoke(OpenDictionary);

            this.dataGridViewDrugs.MouseClick += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        ContextMenu m = new ContextMenu();

                        int currentMouseOverRow = dataGridViewDrugs.HitTest(e.X, e.Y).RowIndex;

                        if (currentMouseOverRow >= 0)
                        {
                            //contextMenuStrip1.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                            contextMenuStrip1.Show(dataGridViewDrugs, new Point(e.X, e.Y));
                        }
                    }
                };

            this.Resize += (sender, e) => CenterProgressBox();

            this.contextMenuStrip1.Items[0].Click += (sender, e) => 
                Invoker.Invoke(ShowDrugDetails);
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

        public event Action OpenDictionary;

        public event Action ShowDrugDetails;

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
                return dataGridViewDrugs.DataSource as IEnumerable<Models.Drugs.PharmacyDrug>;
            }
            set
            {
                dataGridViewDrugs.DataSource = value;
            }
        }

        public Models.Drugs.PharmacyDrug SelectedPharmacyDrug 
        {
            get
            {
                return this.dataGridViewDrugs.SelectedRows[0].DataBoundItem as Models.Drugs.PharmacyDrug;
            }
        }

        public bool IsProgress 
        {
            set
            {
                progressBox.Visible = value;
            }
        }

        #endregion IMainView members


        //-------------------------------------------------


        #region Helper methods


        private void CenterProgressBox()
        {
            var gridWidth = this.dataGridViewDrugs.Width;
            var gridHeight = this.dataGridViewDrugs.Height;

            var progressWidth = this.progressBox.Width;
            var progressHeight = this.progressBox.Height;

            var progressPosX = (gridWidth / 2) - (progressWidth / 2);
            var progressPosY = (gridHeight / 2) - (progressHeight / 2);

            this.progressBox.Location =
                new System.Drawing.Point(progressPosX, progressPosY);
        }


        #endregion Helper methods 
    
    
    }
}
