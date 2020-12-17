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
using Model;
using Implementation;
using System.Data;

namespace Educa.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para StudentList.xaml
    /// </summary>
    public partial class StudentList : Window
    {
        InvoiceImpl invoiceImpl;
        Model.Invoice invoice;
        public StudentList()
        {
            InitializeComponent();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid("");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }
        public void loadGrid(string like)
        {
            try
            {
                invoiceImpl = new InvoiceImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = invoiceImpl.Selectlike(like).DefaultView;


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    invoiceImpl = new InvoiceImpl();
                    invoice = invoiceImpl.Get( id);
                    lblnames.Content = invoice.BussinesName;
                    lblmail.Content =  invoice.IdPayer.ToString();
                    lblid.Text = invoice.IdInvoide.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                loadGrid(txtsearch.Text);
                Ocultar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Administrativo.Controles.Invoice.PaymentView p = new PaymentView(int.Parse(lblid.Text));
                p.Show();
                this.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
