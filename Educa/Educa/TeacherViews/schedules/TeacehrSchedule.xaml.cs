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
using Model;
using Implementation;

namespace Educa.TeacherViews.schedules
{
    /// <summary>
    /// Lógica de interacción para TeacehrSchedule.xaml
    /// </summary>
    public partial class TeacehrSchedule : UserControl
    {
        public TeacehrSchedule()
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
            dgvDatos.ItemsSource = scheduleImpl.SelectSpecificTeacherSchedule(Session.SessionID,"Monday").DefaultView;
            
            dgvDatosmar.ItemsSource = null;
            dgvDatosmar.ItemsSource = scheduleImpl.SelectSpecificTeacherSchedule(Session.SessionID, "Tuesday").DefaultView;

            dgvDatosmier.ItemsSource = null;
            dgvDatosmier.ItemsSource = scheduleImpl.SelectSpecificTeacherSchedule(Session.SessionID, "Wednesday").DefaultView;

            dgvDatosjue.ItemsSource = null;
            dgvDatosjue.ItemsSource = scheduleImpl.SelectSpecificTeacherSchedule(Session.SessionID, "Thursday").DefaultView;

            dgvDatosvier.ItemsSource = null;
            dgvDatosvier.ItemsSource = scheduleImpl.SelectSpecificTeacherSchedule(Session.SessionID, "Friday").DefaultView;

            dgvDatossab.ItemsSource = null;
            dgvDatossab.ItemsSource = scheduleImpl.SelectSpecificTeacherSchedule(Session.SessionID, "Saturday").DefaultView;
        }
    }
}
