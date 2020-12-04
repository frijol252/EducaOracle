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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Implementation;

namespace Educa.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para SubjectsView.xaml
    /// </summary>
    public partial class SubjectsView : UserControl
    {
        Course course;
        CourseImpl courseImpl;
        public SubjectsView()
        {
            InitializeComponent();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        public void LoadDataGrid()
        {
            try
            {
                courseImpl = new CourseImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = courseImpl.Select().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGridlike(string like)
        {
            try
            {
                courseImpl = new CourseImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = courseImpl.Selectlike(like).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Collapsed;
        }


        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {

                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    courseImpl = new CourseImpl();
                    course = courseImpl.Get(id);
                    lblcourse.Content = ""+course.Number;
                    lblletter.Content = course.Letter;
                    lblsection.Content = course.Section;
                    btnStu.IsEnabled = true;
                    btnSubs.IsEnabled = true;
                   
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
                if (txtsearch.Text=="")
                {
                    LoadDataGrid();
                    Ocultar();
                }
                else
                {
                    LoadDataGridlike(txtsearch.Text);
                    Ocultar();
                }
            }
            catch
            {

            }
        }

       

        private void BtnStu_Click(object sender, RoutedEventArgs e)
        {
            Students s = new Students(course.Idcourse);
            s.Show();

        }

        private void BtnSubs_Click(object sender, RoutedEventArgs e)
        {
            SubjectsList sl = new SubjectsList(course.Idcourse);
            sl.Show();
        }

        private void dgvDatos_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
            Ocultar();
        }
    }
}
