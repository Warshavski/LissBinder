﻿using System;
using System.Windows.Forms;

using Escyug.LissBinder.Presentation.Views;

namespace Escyug.LissBinder.App.WinForms
{
    public partial class DetailsForm : BaseForm, IDetailsView
    {
        public DetailsForm()
        {
            InitializeComponent();

            this.buttonClose.Click += (sender, e) => { this.Close(); };
        }


        #region IView members 
       
        public new void Show()
        {
            ShowDialog();
        }

        #endregion IView members
        //---------------------------------------------------------------------


        #region IDetailsView members

        public string DrugName
        {
            set { this.labelName.Text = value; }
        }

        public string Manufacturer
        {
            set { this.labelManufacturer.Text = value; }
        }

        public string Series
        {
            set { this.labelSeries.Text = value; }
        }

        public string Quantity
        {
            set { this.labelQuantity.Text = value; }
        }

        public string Price
        {
            set { this.labelPrice.Text = value; }
        }

        public string Barcode
        {
            set { this.labelBarcode.Text = value; }
        }

        #endregion IDetailsView members
        //---------------------------------------------------------------------


    }
}
