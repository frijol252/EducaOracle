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

namespace Educa
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        
        public Index()
        {
            InitializeComponent();
            labelUser.Content = Session.SessionCurrent;
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            //MoverCursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Home.Inicio());
                    
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Pendiente.Forms());
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Educa.Administrativo.Administrator.Administrator_Views());
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Educa.Administrativo.Students.SubjectsView());
                    break;
                default:
                    break;
            }
        }

        private void MateriasButton_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Controles.Matter.Matters());
        }

        private void HorariosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new Educa.Administrativo.Administrator.Administrator_Views());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CrudTeacher_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            Administrativo.Teacher.TeacherList win = new Administrativo.Teacher.TeacherList();
            win.Show();
        }

        private void ListTeacher_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            Administrativo.Teacher.TeacherWatch win = new Administrativo.Teacher.TeacherWatch();
            win.Show();
        }


        //Falta Terminar este boton
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void ListStudent_Selected(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
