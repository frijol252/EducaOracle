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
namespace Educa.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para RevisionInvoice.xaml
    /// </summary>
    public partial class RevisionInvoice : Window
    {
        int idInvoice;
        InvoiceImpl invoiceImpl;
        public RevisionInvoice(int id)
        {
            this.idInvoice = id;
            InitializeComponent();
        }
        
        void load()
        {
            invoiceImpl = new InvoiceImpl();
            Report1 report1 = new Report1();
            report1.Database.Tables["INVOICEREVIEW"].SetDataSource(invoiceImpl.SelectInvoice(idInvoice));
            crsInvoice.ViewerCore.ReportSource = report1;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }
    }
}
