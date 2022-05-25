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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace Educa.Controles.Matter
{
    /// <summary>
    /// Lógica de interacción para Matters.xaml
    /// </summary>
    public partial class Matters : UserControl
    {
        public Matters()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administrativo.Controles.Invoice.StudentList a = new Administrativo.Controles.Invoice.StudentList();
            a.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Session.SessionRole != "D")
            {
                MessageBox.Show("You can't use this option");
            }
            else
            {
                Administrativo.Controles.Invoice.DosageView d = new Administrativo.Controles.Invoice.DosageView();
                d.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Administrativo.Controles.Invoice.InvoiceView d = new Administrativo.Controles.Invoice.InvoiceView();
            d.Show();
        }
    }
}
