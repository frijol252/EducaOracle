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

namespace Educa.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para Students.xaml
    /// </summary>
    public partial class Students : Window
    {
        int idCourse;
        Student stu;
        StudentImpl studentImpl;
        int dis = 0;
        public Students(int i)
        {
            this.IdCourse = i;
            InitializeComponent();
        }
        #region getters
        public int IdCourse { get => idCourse; set => idCourse = value; }
        #endregion
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }
        public void loadGrid()
        {
            try
            {
                if (dis==0)
                {
                    studentImpl = new StudentImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = studentImpl.Select(idCourse).DefaultView;
                }
                else
                {
                    studentImpl = new StudentImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = studentImpl.SelectDis(idCourse).DefaultView;
                }

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

                    studentImpl = new StudentImpl();
                    stu = studentImpl.Get(IdCourse, id);
                    lblnames.Content = stu.Names;
                    lblmail.Content = stu.Email;
                    lblrude.Content = stu.RudeNumber;
                    lblid.Text = id.ToString();
                    imagesector.Source = new BitmapImage(new Uri(DBImplementation.pathImages + stu.Photo +".png"));

                    btnMod.IsEnabled = true;
                    btnDel.IsEnabled = true;
                    btnDis.IsEnabled = true;
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
                if (dis==0)
                {
                    if (txtsearch.Text == "")
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.Select(idCourse).DefaultView;
                        Ocultar();
                    }
                    else
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.Selectlike(idCourse, txtsearch.Text).DefaultView;
                        Ocultar();
                    }
                }
                else
                {
                    if (txtsearch.Text == "")
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.SelectDis(idCourse).DefaultView;
                        Ocultar();
                    }
                    else
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.SelectDislike(idCourse, txtsearch.Text).DefaultView;
                        Ocultar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd stu = new StudentAdd(idCourse);
            stu.Show();
            this.Close();
        }

        private void BtnMod_Click(object sender, RoutedEventArgs e)
        {
            StudentModif mod = new StudentModif(idCourse,stu.PersonId);
            mod.Show();
            this.Close();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                studentImpl = new StudentImpl();
                int id = stu.PersonId;
                MessageBox.Show(""+id);
                studentImpl.DeleteTransaction(id);

                loadGrid();
                Ocultar();
                MessageBox.Show("Delete Succes");
            }
            catch
            {
                MessageBox.Show("Something was wrong");
            }
        }

        private void BtnDis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                studentImpl = new StudentImpl();
                if (dis==0)
                {
                    studentImpl.UpdateEnabled(int.Parse(lblid.Text), 0);
                }
                else
                {
                    studentImpl.UpdateEnabled(int.Parse(lblid.Text), 1);
                }
                loadGrid();
                Ocultar();
                MessageBox.Show("Student Disable");
            }
            catch
            {
                MessageBox.Show("Something was wrong");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            if (dis == 0) { dis = 1;
                DisStu.Content = "Enabled Students";
                DisStu.Background= (Brush)bc.ConvertFrom("#dfa752");
                btnDel.IsEnabled = false;
                btnDis.IsEnabled = false;
                btnDis.Content = "Enabled";
                btnDis.Background = (Brush)bc.ConvertFrom("#dfa752");
                btnMod.IsEnabled = false;
            }
            else { dis = 0;
                DisStu.Content = "Disabled Students";
                DisStu.Background = (Brush)bc.ConvertFrom("#D15656");
                btnDel.IsEnabled = false;
                btnDis.IsEnabled = false;
                btnDis.Content = "Disabled";
                btnDis.Background = (Brush)bc.ConvertFrom("#D15656");
                btnMod.IsEnabled = false;
            }
            loadGrid();
            Ocultar();
        }
    }
}
