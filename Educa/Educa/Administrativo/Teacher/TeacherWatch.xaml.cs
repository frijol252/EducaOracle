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
namespace Educa.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherWatch.xaml
    /// </summary>
    public partial class TeacherWatch : Window
    {
        Model.Teacher teacher;
        TeacherImpl teacherimpl;
        public TeacherWatch()
        {
            InitializeComponent();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
        #region Grid
        //Control del grid
        public void LoadDataGrid()
        {
            try
            {
                teacherimpl = new TeacherImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = teacherimpl.Select().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGridDis()
        {
            try
            {
                teacherimpl = new TeacherImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = teacherimpl.SelectDis().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ocultar()
        {
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }
        bool enable=true;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {

                    Disabledbtn.IsEnabled = true;
                    subjects.IsEnabled = true;
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    teacherimpl = new TeacherImpl();
                    teacher = teacherimpl.Get(id);
                    txtid.Text = teacher.PersonId.ToString();
                        namenabled.Content = teacher.Names;
                        lastenabled.Content = teacher.LastName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        
        private void BtnDisabledView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controltextos();
                var bc = new BrushConverter();
                if (enable == true)
                {
                    btnDisabledView.Background = (Brush)bc.ConvertFrom("#dfa752");
                    btnDisabledView.Content = "Enabled Teachers";
                    Disabledbtn.Background = (Brush)bc.ConvertFrom("#06ad75");
                    Disabledbtn.Content = "Enabled";
                    enable = false;
                    LoadDataGridDis();
                    ocultar();
                }
                else
                {
                    btnDisabledView.Background = (Brush)bc.ConvertFrom("#06ad75");
                    btnDisabledView.Content = "Disabled Teachers";
                    Disabledbtn.Background = (Brush)bc.ConvertFrom("#dfa752");
                    Disabledbtn.Content = "Disabled";
                    enable = true;
                    LoadDataGrid();
                    ocultar();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public void controltextos()
        {
            namenabled.Content = "";
            lastenabled.Content = "";
        }

        private void Disabledbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (enable == true)
                {
                    int id = int.Parse(txtid.Text);
                    teacher = new Model.Teacher(id,"","");
                    teacherimpl = new TeacherImpl();
                    int res = teacherimpl.Disabled(teacher);
                    if (res > 0)
                    {
                        MessageBox.Show("Teacher Disable successfully!!!");
                        LoadDataGrid();
                        ocultar();
                    }
                    else MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
                }
                else
                {
                    int id = int.Parse(txtid.Text);
                    teacher = new Model.Teacher(id,"","");
                    teacherimpl = new TeacherImpl();
                    int res = teacherimpl.Enabled(teacher);
                    if (res > 0)
                    {
                        MessageBox.Show("Teacher Enable successfully!!!");
                        LoadDataGridDis();
                        ocultar();
                    }
                    else MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    teacherimpl = new TeacherImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = teacherimpl.Select().DefaultView;
                    ocultar();
                }
                else
                {
                    teacherimpl = new TeacherImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = teacherimpl.SelectSearch(txtsearch.Text).DefaultView;
                    ocultar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            TeacherSubject ts = new TeacherSubject(int.Parse(teacher.Id.ToString()));
            ts.Show();
            this.Close();
        }

        private void dgvDatos_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
            ocultar();
        }
    }
}
