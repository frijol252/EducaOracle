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
using System.Data;

namespace Educa.Controles.Horarios
{
    /// <summary>
    /// Lógica de interacción para HorarioList.xaml
    /// </summary>
    public partial class HorarioList : Window
    {
        Horario horario;
        HorarioImpl implhorario;
        public HorarioList()
        {
            InitializeComponent();
            
        }
        //Poder Mover La Pantalla
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        

        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    byte id = byte.Parse(dataRow.Row.ItemArray[0].ToString());
                    
                    implhorario = new HorarioImpl();
                    horario = implhorario.Get(id);
                    MessageBox.Show(horario.estado.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //Select
        public void LoadDataGrid()
        {
            try
            {
                implhorario = new HorarioImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = implhorario.Select().DefaultView;
                MessageBox.Show("a");
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
