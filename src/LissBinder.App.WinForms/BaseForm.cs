using System.Windows.Forms;

using Escyug.LissBinder.Presentation.Common;


namespace Escyug.LissBinder.App.WinForms
{
    /// <summary>
    /// Base class for all forms
    /// </summary>
    public partial class BaseForm : Form, IView
    {
        public BaseForm()
        {
            
        }


        #region IView members 


        /// <summary>
        /// Show error message
        /// </summary>
        public string Error
        {
            set
            {
                MessageBox.Show(value, "Application error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Show notify message
        /// </summary>
        public string Notify
        {
            set
            {
                MessageBox.Show(value, "Application information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion IView members

    }
}
