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
using System.Windows.Shapes;
using Implementation;
using Model;

namespace Educa.TeacherViews.Subjects
{
    /// <summary>
    /// Lógica de interacción para StudenGrade.xaml
    /// </summary>
    public partial class StudenGrade : Window
    {
        public StudenGrade(int id,int trimester)
        {
            this.idClass = id;
            this.trimester = trimester;
            InitializeComponent();
        }
        int trimester;
        int idGrade;
        int idClass;
        GradeImpl gradeImpl;
        Grade grade;
        #region Controls
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ocultar();
        }
        public void ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
            dgvDatos.Columns[8].Visibility = Visibility.Hidden;
        }
        public void loadGrid()
        {
            gradeImpl = new GradeImpl();
            dgvDatos.ItemsSource = null;
            switch (trimester)
            {
                case 1:
                    dgvDatos.ItemsSource = gradeImpl.SelectStudentsFirst(idClass).DefaultView;
                    break;
                case 2:
                    dgvDatos.ItemsSource = gradeImpl.SelectStudentsSecond(idClass).DefaultView;
                    break;
                case 3:
                    dgvDatos.ItemsSource = gradeImpl.SelectStudentsThird(idClass).DefaultView;
                    break;
            }
            
        }
        #endregion
        int idTotal;
        #region grid
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    idGrade = id;
                    lblstudent.Content = dataRow.Row.ItemArray[1].ToString();
                    
                    txtGrade1.Text = dataRow.Row.ItemArray[2].ToString();
                    if (Convert.ToDouble(txtGrade1.Text)>0) { txtGrade1.IsEnabled = false;} else { txtGrade1.IsEnabled = true; }
                    txtGrade2.Text = dataRow.Row.ItemArray[3].ToString();
                    if (Convert.ToDouble(txtGrade2.Text) > 0) { txtGrade2.IsEnabled = false; } else { txtGrade2.IsEnabled = true; }
                    txtGrade3.Text = dataRow.Row.ItemArray[4].ToString();
                    if (Convert.ToDouble(txtGrade3.Text) > 0) { txtGrade3.IsEnabled = false; } else { txtGrade3.IsEnabled = true; }
                    txtGrade4.Text = dataRow.Row.ItemArray[5].ToString();
                    if (Convert.ToDouble(txtGrade4.Text) > 0) { txtGrade4.IsEnabled = false; } else { txtGrade4.IsEnabled = true; }
                    txtTest.Text = dataRow.Row.ItemArray[6].ToString();
                    if (Convert.ToDouble(txtTest.Text) > 0) { txtTest.IsEnabled = false; } else { txtTest.IsEnabled = true; }
                    idTotal=int.Parse(dataRow.Row.ItemArray[8].ToString());
                    btnAddGrade.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
        #region search
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    if (txtsearch.Text == "")
            //    {
            //        clImpl = new ClassImpl();
            //        dgvDatos.ItemsSource = null;
            //        dgvDatos.ItemsSource = clImpl.Select(Course).DefaultView;
            //        Ocultar();
            //    }
            //    else
            //    {
            //        clImpl = new ClassImpl();
            //        dgvDatos.ItemsSource = null;
            //        dgvDatos.ItemsSource = clImpl.Selectlike(Course, txtsearch.Text).DefaultView;
            //        Ocultar();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        #endregion

        private void BtnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region controlgrade
                double grade1 =Convert.ToDouble(txtGrade1.Text);
                double grade2 = Convert.ToDouble(txtGrade2.Text);
                double grade3 = Convert.ToDouble(txtGrade3.Text);
                double grade4 = Convert.ToDouble(txtGrade4.Text);
                double gradetest = Convert.ToDouble(txtTest.Text);
                double gradeaverage;
                double first=(grade1 * 0.175) +(grade2 * 0.175) +(grade3 * 0.175) +(grade4 * 0.175);
                double second = (gradetest * 0.3);
                gradeaverage = first + second;
                double total=0;
                double sumatotal = 0;
                switch (trimester)
                {
                    case 1:
                        total = gradeaverage * 0.35;
                        break;
                    case 2:
                        total = gradeaverage * 0.35; 
                        break;
                    case 3:
                        total = gradeaverage * 0.30; 
                        break;
                }
                grade1 = Math.Round(grade1, 0);
                grade2 = Math.Round(grade2, 0);
                grade3 = Math.Round(grade3, 0);
                grade4 = Math.Round(grade4, 0);
                gradetest = Math.Round(gradetest, 0);
                gradeaverage = Math.Round(gradeaverage, 0);
                total = Math.Round(total, 2);
                grade = new Grade(idGrade, grade1, grade2, grade3, grade4, gradetest, gradeaverage, idTotal);
                gradeImpl = new GradeImpl();
                DataTable dt = new DataTable();
                dt = gradeImpl.SelectTotal(idTotal);
                switch (trimester)
                {
                    case 1:
                        foreach (DataRow d in dt.Rows)
                        {
                            sumatotal = total + Convert.ToDouble(d[2].ToString()) + Convert.ToDouble(d[3].ToString());
                        }
                        gradeImpl.UpdateTransactionFirst(grade,total, sumatotal);
                        break;
                    case 2:
                        foreach (DataRow d in dt.Rows)
                        {
                            sumatotal = total + Convert.ToDouble(d[1].ToString()) + Convert.ToDouble(d[3].ToString());
                        }
                        gradeImpl.UpdateTransactionSecond(grade, total, sumatotal);
                        break;
                    case 3:
                        foreach (DataRow d in dt.Rows)
                        {
                            sumatotal = total + Convert.ToDouble(d[1].ToString()) + Convert.ToDouble(d[2].ToString());
                        }
                        gradeImpl.UpdateTransactionThird(grade, total, sumatotal);
                        break;
                }
                
                #endregion


                if (grade1 > 0) { txtGrade1.IsEnabled = false; } else { txtGrade1.IsEnabled = true; }
                if (grade2 > 0) { txtGrade2.IsEnabled = false; } else { txtGrade2.IsEnabled = true; }
                if (grade3 > 0) { txtGrade3.IsEnabled = false; } else { txtGrade3.IsEnabled = true; }
                if (grade4 > 0) { txtGrade4.IsEnabled = false; } else { txtGrade4.IsEnabled = true; }
                if (gradetest > 0) { txtTest.IsEnabled = false; } else { txtTest.IsEnabled = true; }
                MessageBox.Show("Upload Grades Succesfuly");
                loadGrid();
                ocultar();

            }
            catch
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }
    }
}
