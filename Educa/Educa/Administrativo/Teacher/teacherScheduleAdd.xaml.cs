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

namespace Educa.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para teacherScheduleAdd.xaml
    /// </summary>
    public partial class teacherScheduleAdd : Window
    {
        int idTeacher;
        ClassImpl classImpl;
        Class classesita;
        public teacherScheduleAdd(int id)
        {
            this.idTeacher = id;
            InitializeComponent();
            lblnames.Content = idTeacher;
        }
        #region control
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        public void LoadDataGrid()
        {
            try
            {
                classImpl = new ClassImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = classImpl.SelectTeacherAdd(idTeacher).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGridlike(string like)
        {
            try
            {
                classImpl = new ClassImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = classImpl.SelectTeacherAdd(like).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void ocultar()
        {
            try
            {
                dgvDatos.Columns[0].Visibility = Visibility.Hidden;
            }
            catch
            {

            }
        }
        

        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ocultar();
        }

        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadDataGrid();
                    ocultar();
                }
                else
                {
                    LoadDataGridlike(txtsearch.Text);
                    ocultar();
                }
            }
            catch
            {

            }
        }
        #endregion
        #region selected
        int idclass;
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    idclass = id;
                    lblnames.Content = dataRow.Row.ItemArray[1];
                    lblcat.Content = dataRow.Row.ItemArray[2];
                    btnAdd.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion
        private void BtnAdd_Click_1(object sender, RoutedEventArgs e)
        {
              try
                {
                //names, lastName, secondLastName, address, phone, birthDate,gender,startDate,email
                classesita = new Class(idclass, idTeacher, 0, 0);
                classImpl = new ClassImpl();
                classImpl.InsertTeacherSubject(classesita);
                LoadDataGrid();
                ocultar();
                MessageBox.Show("Subject Add successfully");
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
                }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TeacherSubject ts = new TeacherSubject(idTeacher);
                ts.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }
    }
}
