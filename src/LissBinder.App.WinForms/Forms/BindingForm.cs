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


        #region IView members

        public new void Show()
        {
            ShowDialog();
        }

        #endregion IView members
        //---------------------------------------------------------------------


        #region IBindingView members

        public event Func<Task> BindingInitializeAsync;

        public bool IsBusy
        {
            get
            {
                return pictureBoxProgress.Visible;
            }
            set
            {
                pictureBoxProgress.Visible = value;
            }
        }

        #endregion IBindingView members
        //---------------------------------------------------------------------

    }
}
