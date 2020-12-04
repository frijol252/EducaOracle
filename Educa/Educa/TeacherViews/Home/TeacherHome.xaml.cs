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

namespace Educa.TeacherViews.Home
{
    /// <summary>
    /// Lógica de interacción para TeacherHome.xaml
    /// </summary>
    public partial class TeacherHome : Window
    {
        public TeacherHome()
        {
            InitializeComponent();
            labelUser.Content = Session.SessionCurrent;
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            //MoverCursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();

                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }

        private void ListTeacher_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
        }

        private void ListTeacher_Click_1(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Educa.TeacherViews.schedules.TeacehrSchedule());
        }

        private void CrudTeacher_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Educa.TeacherViews.Subjects.TeacherSubjectView());
        }
    }
}
