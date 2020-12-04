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
    /// Lógica de interacción para TeacherSubject.xaml
    /// </summary>
    public partial class TeacherSubject : Window
    {
        int idTeacher;
        Class @class;
        ClassImpl classImpl;
        public TeacherSubject(int id)
        {
            this.idTeacher = id;
            InitializeComponent();
        }

        public void loadGrid()
        {
            try
            {
                classImpl = new ClassImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = classImpl.SelectTeacherActive(idTeacher).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        int ids;
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    ids = id;
                    btnDel.IsEnabled = true;
                    lblnames.Content = dataRow.Row.ItemArray[1];
                    lblcat.Content= dataRow.Row.ItemArray[2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            teacherScheduleAdd tsa = new teacherScheduleAdd(idTeacher);
            tsa.Show();
            this.Close();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            @class = new Class(ids, 0, 0, 0);
            classImpl = new ClassImpl();
            classImpl.DelTeacherSubject(@class);
            loadGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TeacherWatch tw = new TeacherWatch();
            tw.Show();
            this.Close();
        }
    }
}
