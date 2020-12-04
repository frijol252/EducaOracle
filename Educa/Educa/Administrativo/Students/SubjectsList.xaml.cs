using System;
using System.Collections.Generic;
using System.Data;
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

namespace Educa.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para SubjectsList.xaml
    /// </summary>
    public partial class SubjectsList : Window
    {
        int Course;
        Class cl;
        ClassImpl clImpl;
        public SubjectsList(int Course)
        {
            this.Course = Course;
            InitializeComponent();
        }
        #region control 
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Pe
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }
        public void Ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }
        public void loadGrid()
        {
            try
            {
                clImpl = new ClassImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = clImpl.Select(Course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        int ids=0;
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    ids = id;
                    btnDel.IsEnabled=true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region search
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    clImpl = new ClassImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = clImpl.Select(Course).DefaultView;
                    Ocultar();
                }
                else
                {
                    clImpl = new ClassImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = clImpl.Selectlike(Course, txtsearch.Text).DefaultView;
                    Ocultar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SubjectAdd sa = new SubjectAdd(Course);
            sa.Show();
            this.Close();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clImpl = new ClassImpl();
                clImpl.DeleteTransaction(ids);

                loadGrid();
                Ocultar();
                MessageBox.Show("Class Delete Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }
    }
}
