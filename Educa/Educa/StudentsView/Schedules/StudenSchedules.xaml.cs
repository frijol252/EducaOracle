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

namespace Educa.StudentsView.Schedules
{
    /// <summary>
    /// Lógica de interacción para StudenSchedules.xaml
    /// </summary>
    public partial class StudenSchedules : UserControl
    {
        public StudenSchedules()
        {
            InitializeComponent();
        }
        ScheduleImpl scheduleImpl;
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            loadGrids();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void loadGrids()
        {
            scheduleImpl = new ScheduleImpl();
            dgvDatos.ItemsSource = null;
            dgvDatos.ItemsSource = scheduleImpl.SelectSpecificStudentSchedule(Session.SessionID, "Monday").DefaultView;

            dgvDatosmar.ItemsSource = null;
            dgvDatosmar.ItemsSource = scheduleImpl.SelectSpecificStudentSchedule(Session.SessionID, "Tuesday").DefaultView;

            dgvDatosmier.ItemsSource = null;
            dgvDatosmier.ItemsSource = scheduleImpl.SelectSpecificStudentSchedule(Session.SessionID, "Wednesday").DefaultView;

            dgvDatosjue.ItemsSource = null;
            dgvDatosjue.ItemsSource = scheduleImpl.SelectSpecificStudentSchedule(Session.SessionID, "Thursday").DefaultView;

            dgvDatosvier.ItemsSource = null;
            dgvDatosvier.ItemsSource = scheduleImpl.SelectSpecificStudentSchedule(Session.SessionID, "Friday").DefaultView;

            dgvDatossab.ItemsSource = null;
            dgvDatossab.ItemsSource = scheduleImpl.SelectSpecificStudentSchedule(Session.SessionID, "Saturday").DefaultView;
        }
    }
}
