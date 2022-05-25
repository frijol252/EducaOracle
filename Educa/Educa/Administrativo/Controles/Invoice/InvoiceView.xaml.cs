using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Implementation;
using Model;

namespace Educa.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : Window
    {
        int id;
        string motive;
        double amount;
        public InvoiceView()
        {
            InitializeComponent();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnAnular_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to cancel invoice?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
                if (txtMotive.Text!="")
                {
                    Model.Invoice invoice = new Model.Invoice();
                    invoice.IdInvoide = id;
                    invoice.Total = amount;
                    invoice.BussinesName = txtMotive.Text;
                    invoiceImpl = new InvoiceImpl();
                    invoiceImpl.UpdateTransaction(invoice);
                }
                else MessageBox.Show("Please describe motive");
            }
        }

        private void txtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            load(txtCode.Text);
        }
        Model.Invoice invoice;
        InvoiceImpl invoiceImpl;
        void load(string code)
        {
            
            invoiceImpl = new InvoiceImpl();
            invoice = invoiceImpl.GetInvoice(code);
            id = invoice.IdInvoide;
            amount = invoice.Total;
            Report1 report1 = new Report1();
            report1.Database.Tables["INVOICEREVIEW"].SetDataSource(invoiceImpl.SelectInvoices(code));
            crsInvoice.ViewerCore.ReportSource = report1;
            btnAnular.IsEnabled = true;
        }
    }
}
