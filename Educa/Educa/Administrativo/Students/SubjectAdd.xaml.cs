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
using System.Data;
using Implementation;
using System.Data;

namespace Educa.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para SubjectAdd.xaml
    /// </summary>
    public partial class SubjectAdd : Window
    {
        int course;
        public SubjectAdd(int course)
        {
            this.course = course;
            InitializeComponent();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        #region grid
        Subject sub;
        SubjectImpl subjectImpl;
        Schedule sch;
        ScheduleImpl scheduleImpl;
        ClassImpl classs;
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
            loadGridSche();
            loadGridSchemar();
            loadGridSchemier();
            loadGridSchejue();
            loadGridSchevier();
            loadGridSchesab();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
            Ocultarsch();
        }
        public void Ocultar()
        {
            dgvSub.Columns[0].Visibility = Visibility.Collapsed;
            dgvDatos.Columns[2].Visibility = Visibility.Hidden;
            dgvDatos.Columns[3].Visibility = Visibility.Hidden;
            dgvDatosmar.Columns[0].Visibility = Visibility.Hidden;
            dgvDatosmar.Columns[2].Visibility = Visibility.Hidden;
            dgvDatosmar.Columns[3].Visibility = Visibility.Hidden;
            dgvDatosmier.Columns[0].Visibility = Visibility.Hidden;
            dgvDatosmier.Columns[2].Visibility = Visibility.Hidden;
            dgvDatosmier.Columns[3].Visibility = Visibility.Hidden;
            dgvDatosjue.Columns[0].Visibility = Visibility.Hidden;
            dgvDatosjue.Columns[2].Visibility = Visibility.Hidden;
            dgvDatosjue.Columns[3].Visibility = Visibility.Hidden;
            dgvDatosvier.Columns[0].Visibility = Visibility.Hidden;
            dgvDatosvier.Columns[2].Visibility = Visibility.Hidden;
            dgvDatosvier.Columns[3].Visibility = Visibility.Hidden;
            dgvDatossab.Columns[0].Visibility = Visibility.Hidden;
            dgvDatossab.Columns[2].Visibility = Visibility.Hidden;
            dgvDatossab.Columns[3].Visibility = Visibility.Hidden;
        }
        public void Ocultarsch()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }
        public void loadGrid()
        {
            try
            {
                subjectImpl = new SubjectImpl();
                dgvSub.ItemsSource = null;
                dgvSub.ItemsSource = subjectImpl.Selectasd(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSche()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = scheduleImpl.Select(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSchemar()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatosmar.ItemsSource = null;
                dgvDatosmar.ItemsSource = scheduleImpl.Selectmar(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSchemier()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatosmier.ItemsSource = null;
                dgvDatosmier.ItemsSource = scheduleImpl.Selectmier(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSchejue()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatosjue.ItemsSource = null;
                dgvDatosjue.ItemsSource = scheduleImpl.Selectjue(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSchevier()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatosvier.ItemsSource = null;
                dgvDatosvier.ItemsSource = scheduleImpl.Selectvier(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSchesab()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatossab.ItemsSource = null;
                dgvDatossab.ItemsSource = scheduleImpl.Selectsab(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        #region search
        //private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtsearch.Text == "")
        //        {
        //            scheduleImpl = new ScheduleImpl();
        //            dgvDatos.ItemsSource = null;
        //            dgvDatos.ItemsSource = scheduleImpl.Select(course).DefaultView;
        //            Ocultarsch();
        //        }
        //        else
        //        {
        //            scheduleImpl = new ScheduleImpl();
        //            dgvDatos.ItemsSource = null;
        //            dgvDatos.ItemsSource = scheduleImpl.Selectlike(course,txtsearch.Text).DefaultView;
        //            Ocultarsch();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        #endregion

        int idSchedule = 0;
        int idSubject = 0;
        int count = 1;
        string day = "";
        string hours = "";
        string hourf = "";
        List<int> lista=new List<int>();
        private void DgvSub_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvSub.Items.Count > 0 && dgvSub.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvSub.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    idSubject = id;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #region comboSelected
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    string tal = dataRow.Row.ItemArray[1].ToString();
                    if (tal == "X") MessageBox.Show("This schedule is occuped");
                    else
                    {
                        hours = dataRow.Row.ItemArray[2].ToString();
                        hourf = dataRow.Row.ItemArray[3].ToString();
                        day = "Monday";
                        idSchedule = id;
                        Add.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DgvDatosmar_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatosmar.Items.Count > 0 && dgvDatosmar.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatosmar.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    string tal = dataRow.Row.ItemArray[1].ToString();
                    if (tal == "X") MessageBox.Show("This schedule is occuped");
                    else
                    {
                        hours = dataRow.Row.ItemArray[2].ToString();
                        hourf = dataRow.Row.ItemArray[3].ToString();
                        day = "Tuesday";
                        idSchedule = id;
                        Add.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DgvDatosmier_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatosmier.Items.Count > 0 && dgvDatosmier.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatosmier.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    string tal = dataRow.Row.ItemArray[1].ToString();
                    if (tal == "X") MessageBox.Show("This schedule is occuped");
                    else
                    {
                        hours = dataRow.Row.ItemArray[2].ToString();
                        hourf = dataRow.Row.ItemArray[3].ToString();
                        day = "Wednesday";
                        idSchedule = id;
                        Add.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DgvDatosjue_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatosjue.Items.Count > 0 && dgvDatosjue.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatosjue.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    string tal = dataRow.Row.ItemArray[1].ToString();
                    if (tal == "X") MessageBox.Show("This schedule is occuped");
                    else
                    {
                        hours = dataRow.Row.ItemArray[2].ToString();
                        hourf = dataRow.Row.ItemArray[3].ToString();
                        day = "Thursday";
                        idSchedule = id;
                        Add.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DgvDatosvie_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatosvier.Items.Count > 0 && dgvDatosvier.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatosvier.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    string tal = dataRow.Row.ItemArray[1].ToString();
                    if (tal == "X") MessageBox.Show("This schedule is occuped");
                    else
                    {
                        hours = dataRow.Row.ItemArray[2].ToString();
                        hourf = dataRow.Row.ItemArray[3].ToString();
                        day = "Friday";
                        idSchedule = id;
                        Add.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DgvDatossab_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatossab.Items.Count > 0 && dgvDatossab.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatossab.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    string tal = dataRow.Row.ItemArray[1].ToString();
                    if (tal == "X") MessageBox.Show("This schedule is occuped");
                    else
                    {
                        hours = dataRow.Row.ItemArray[2].ToString();
                        hourf = dataRow.Row.ItemArray[3].ToString();
                        day = "Saturday";
                        idSchedule = id;
                        Add.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
        private void BtnSelectSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idSubject == 0){ MessageBox.Show("Please select one Subject"); }
                else
                {
                    dgvSub.IsEnabled = false;
                    btnSelectSubject.IsEnabled = false;
                    dgvDatos.IsEnabled = true;
                    dgvDatosjue.IsEnabled = true;
                    dgvDatosmar.IsEnabled = true;
                    dgvDatosmier.IsEnabled = true;
                    dgvDatossab.IsEnabled = true;
                    dgvDatosvier.IsEnabled = true;
                }
            }
            catch
            {

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool tryed=false;
            foreach (var l in lista)
            {
                if (l == idSchedule) tryed = true;
                
            }
            if (tryed == false)
            {
                lista.Add(idSchedule);
                lstSubs.Items.Add(day + " / " + hours + " - " + hourf);
                Addsubject.IsEnabled = true;
                count++;
            }
            else
            {
                MessageBox.Show("This schedule is ready");
            }
        }

        private void Addsubject_Click(object sender, RoutedEventArgs e)
        {
            classs = new ClassImpl();
            classs.InsertTransaction(idSubject,lista,count,course);
            SubjectsList sl = new SubjectsList(course);
            sl.Show();
            this.Close();
        }

        private void dgvSub_Loaded(object sender, RoutedEventArgs e)
        {
            subjectImpl = new SubjectImpl();
            btnSelectSubject.Content = subjectImpl.Selectasd(course).Rows.Count.ToString();
        }
    }
}
