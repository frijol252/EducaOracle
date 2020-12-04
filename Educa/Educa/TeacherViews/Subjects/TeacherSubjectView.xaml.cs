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
using Implementation;
using Model;

namespace Educa.TeacherViews.Subjects
{
    /// <summary>
    /// Lógica de interacción para TeacherSubjectView.xaml
    /// </summary>
    public partial class TeacherSubjectView : UserControl
    {
        public TeacherSubjectView()
        {
            InitializeComponent();
        }
        Subject subject;
        SubjectImpl subjectImpl;

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }
        public void loadGrid()
        {
            subjectImpl = new SubjectImpl();
            dgvDatos.ItemsSource = null;
            dgvDatos.ItemsSource = subjectImpl.SelectSpecificTeacherSubject(Session.SessionID).DefaultView;
        }

        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    lblid.Text = id.ToString();
                    lblnames.Content = dataRow.Row.ItemArray[1].ToString();
                    lblcat.Content = dataRow.Row.ItemArray[2].ToString();
                    btnFirst.IsEnabled = true;
                    btnSecond.IsEnabled = true;
                    btnThird.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void loadGrades(int numero)
        {
            StudenGrade sg = new StudenGrade(int.Parse(lblid.Text.ToString()),numero);
            sg.Show();
        }

        private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            loadGrades(1);
        }

        private void BtnSecond_Click(object sender, RoutedEventArgs e)
        {
            loadGrades(2);
        }

        private void BtnThird_Click(object sender, RoutedEventArgs e)
        {
            loadGrades(3);
        }
    }
}
