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
namespace Educa.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para DosageView.xaml
    /// </summary>
    public partial class DosageView : Window
    {
        Dosage dosage;
        DosageImpl dosageImpl;
        public DosageView()
        {
            InitializeComponent();
        }
        #region controls
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            LoadDosage();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
        #endregion
        void LoadDosage()
        {
            try
            {

                dosageImpl = new DosageImpl();
                dosage = dosageImpl.Get();
                lblAuthotization.Content = dosage.NroAuthorization.ToString();
                lbldosageKey.Content = dosage.DosageKey;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dosage = new Dosage();
                dosage.NroAuthorization = long.Parse(txtNroAuthorizacion.Text);
                dosage.DosageKey = txtTest.Text;

                dosageImpl = new DosageImpl();
                dosageImpl.InsertTransaction(dosage);
                MessageBox.Show("Dosage Update");
                LoadDosage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something was wrong");
            }
        }
    }
}
