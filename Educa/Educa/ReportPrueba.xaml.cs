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
namespace Educa
{
    /// <summary>
    /// Lógica de interacción para ReportPrueba.xaml
    /// </summary>
    public partial class ReportPrueba : Window
    {
        public ReportPrueba()
        {
            InitializeComponent();
            asd();
        }
        InvoiceImpl courseImpl;
        void asd()
        {
            courseImpl = new InvoiceImpl();
            Report1 report1 = new Report1();
            report1.Database.Tables["INVOICEREVIEW"].SetDataSource(courseImpl.SelectInvoice(47));
            CRS.ViewerCore.ReportSource = report1;
        }
    }
}
