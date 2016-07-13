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
    public partial class LoginForm : Form, ILoginView
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        //-------------------------------------------------


        #region IView members 


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


        #region ILoginView members


        public event Action LoginExecute;

        public string Login
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

        public string Password
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }


        #endregion ILoginView members
    }
}
