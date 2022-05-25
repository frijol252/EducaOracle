using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Lógica de interacción para PaymentView.xaml
    /// </summary>
    public partial class PaymentView : Window
    {
        int idStudent;
        double Amountt;
        PaymentImpl paymentImpl;
        Model.Payment payment;
        Payer payer;
        PayerImpl payerImpl;
        bool exists = true;
        int idPayer = -1;
        int idPayment = -1;
        double total;
        List<Model.Payment> paymentlist = new List<Model.Payment>();
        InvoiceImpl invoiceImpl;
        public PaymentView(int id)
        {
            InitializeComponent();
            this.idStudent = id;
            
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }
        void LoadGrid()
        {
            try
            {
                paymentImpl = new PaymentImpl();
                dgvSub.ItemsSource = null;
                dgvSub.ItemsSource = paymentImpl.Select(idStudent).DefaultView;


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        void Ocultar()
        {
            dgvSub.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                payerImpl = new PayerImpl();
                payerImpl.InsertTransaction(txtnit.Text, txtBuss.Text);
                MessageBox.Show("Payer Insert");
                btnAdd.Height = 0; btnAdd.Width = 0;
                lblbuss.Height = 40; lblbuss.Width = 164;
                txtBuss.Height = 0; txtBuss.Width = 0;
                exists = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something was wrong");
            }
        }

        private void BtnSelectSubject_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool exists = false;
                foreach (var l in paymentlist)
                {
                    if (l.IdPayment==payment.IdPayment)
                    {
                        exists = true;
                    }
                }
                if (exists) MessageBox.Show("Payment already");
                else 
                {
                    paymentlist.Add(payment); lstSubs.Items.Add("" + payment.Detail); 
                    Addsubject.IsEnabled = true; total = total+payment.Balance;
                    lblamount.Content = ""+total; 
                }
            }
            catch
            {
                throw;
            }
        }

        private void Addsubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtamount.Text != "")
                {
                    if (Convert.ToDouble(txtamount.Text)<=total)
                    {
                        if (idPayer == -1)
                        {
                            MessageBox.Show("Select some Nit Payer");
                        }
                        else
                        {
                            double amount = Convert.ToDouble(txtamount.Text);
                            double amountsend = Convert.ToDouble(txtamount.Text);
                            foreach (var l in paymentlist)
                            {
                                if (l.Balance <= amount)
                                {
                                    
                                    l.Paidout = amount;
                                    l.Status = 2;
                                    amount = amount - l.Balance;
                                }
                                else
                                {
                                    l.Paidout = amount;
                                    l.Status = 1;
                                }
                            }
                            int idaverage = DBImplementation.GetIdentityFromTable("INVOICE");
                            DBImplementation.ResetIdentFromTable("INVOICE");
                            invoiceImpl = new InvoiceImpl();
                            Debug.Listeners.Clear();
                            Debug.Listeners.Add(Logss.LogInternalActivities);
                            Debug.WriteLine(string.Format("{0} Info: Start Insert Payment " + idaverage + "{1}", DateTime.Now, Session.SessionID));
                            Debug.Flush();
                            invoiceImpl.InsertTransaction(paymentlist, amountsend, payer);
                            MessageBox.Show("Payment Succesfuly");
                            
                            RevisionInvoice r = new RevisionInvoice(idaverage);
                            r.Show();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("You can't pay more");
                    }
                }
                else
                {
                    MessageBox.Show("Please don't forgot amount");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DgvSub_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvSub.Items.Count > 0 && dgvSub.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvSub.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    payment = new Model.Payment();
                    payment.IdPayment = id;
                    payment.Detail = dataRow.Row.ItemArray[1].ToString();
                    payment.Balance = Convert.ToDouble(dataRow.Row.ItemArray[4].ToString());
                    Add.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtnit_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                if (exists)
                {
                    payerImpl = new PayerImpl();
                    payer = payerImpl.Get(txtnit.Text);

                    if (payer.IdPayer > -1)
                    {
                        lblbuss.Content = payer.BusinessName;
                        idPayer = payer.IdPayer;
                    }
                    else
                    {
                        btnAdd.Height = 40; btnAdd.Width = 100;
                        lblbuss.Height = 0; lblbuss.Width = 0;
                        txtBuss.Height = 40; txtBuss.Width = 164;
                        exists = false;
                    }
                }
                else
                {
                    btnAdd.Height = 0; btnAdd.Width = 0;
                    lblbuss.Height = 40; lblbuss.Width = 164;
                    txtBuss.Height = 0; txtBuss.Width = 0;
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
