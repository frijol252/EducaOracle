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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Implementation;
using Model;

namespace Educa.StudentsView.Grades
{
    /// <summary>
    /// Lógica de interacción para StudentsGradeView.xaml
    /// </summary>
    public partial class StudentsGradeView : UserControl
    {
        public StudentsGradeView()
        {
            InitializeComponent();
        }
        GradeImpl gradeImpl;
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            loadGrids();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void loadGrids()
        {
            gradeImpl = new GradeImpl();
            dgvDatos.ItemsSource = null;
            dgvDatos.ItemsSource = gradeImpl.SelectStudentsGrades(Session.SessionID).DefaultView;
            
        }
    }
}
