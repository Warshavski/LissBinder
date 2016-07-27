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
    public partial class LoginForm : BaseForm, ILoginView
    {
        private readonly ApplicationContext _context;

        public LoginForm(ApplicationContext context)
        {
            _context = context;

            InitializeComponent();

            this.buttonSignIn.Click += async (sender, e) => await Invoker.InvokeAsync(SignInAsync);

            this.buttonCancel.Click += (sender, e) => { this.Close(); };
        }



        //---------------------------------------------------------------------


        #region IView member

        
        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(_context);
        }


        #endregion IView member


        //---------------------------------------------------------------------


        #region ILoginView members


        public event Func<Task> SignInAsync;

        public string Login
        {
            get
            {
                return textBoxLogin.Text;
            }
            set
            {
                textBoxLogin.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return textBoxPassword.Text;
            }
            set
            {
                textBoxPassword.Text = value;
            }
        }

        public bool IsBusy 
        {
            get
            {
                return pictureBoxLoading.Visible;
            }
            set
            {
                pictureBoxLoading.Visible = value;
            }
        }

        #endregion ILoginView members

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
