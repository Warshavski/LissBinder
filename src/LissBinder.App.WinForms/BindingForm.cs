using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Escyug.LissBinder.Presentation.Common;
using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.App.WinForms
{
    public partial class BindingForm : BaseForm, IBindingView
    {
        public BindingForm()
        {
            InitializeComponent();

            this.Load += async (sender, e) => await Invoker.InvokeAsync(BindingInitializeAsync);
        }

        public new void Show()
        {
            ShowDialog();
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = global::Escyug.LissBinder.App.WinForms.Properties.Resources._35__1_;
            this.pictureBox1.Location = new System.Drawing.Point(53, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 52);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BindingForm
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(158, 77);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BindingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        public bool IsBusy
        {
            get
            {
                return pictureBox1.Visible;
            }
            set
            {
                pictureBox1.Visible = value;
            }
        }

        public event Func<Task> BindingInitializeAsync;
    }
}
