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

            this.button1.Click += async (sender, e) => 
                await Invoker.InvokeAsync(LoginExecuteAsync);
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


        public event Func<Task> LoginExecuteAsync;

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
