using System;
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
                pictureBoxLogo.Visible = !value;
                pictureBoxLoading.Visible = value;
            }
        }

        #endregion ILoginView members
        //---------------------------------------------------------------------

    }
}
