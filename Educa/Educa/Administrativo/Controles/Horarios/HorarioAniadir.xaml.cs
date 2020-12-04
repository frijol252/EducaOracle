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

namespace Educa.Controles.Horarios
{
    /// <summary>
    /// Lógica de interacción para HorarioAniadir.xaml
    /// </summary>
    public partial class HorarioAniadir : Window
    {
        Horario horario;
        HorarioImpl implhorario;
        public HorarioAniadir()
        {
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

        private void AceptarInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                horario = new Horario(comboday.Text, TimeInicio.SelectedTime.Value, TimeFin.SelectedTime.Value);
                implhorario= new HorarioImpl();
                int res = implhorario.Insert(horario);
                if (res > 0)
                {
                    MessageBox.Show("Teacher insert successfully!!!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AceptarModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                horario = new Horario(TimeInicioModif.SelectedTime.Value, TimeFinModif.SelectedTime.Value, ComboModif.Text, short.Parse(txtId.Text));
                implhorario = new HorarioImpl();
                int res = implhorario.Update(horario);
                if (res > 0)
                {
                    MessageBox.Show("Horario modificado con éxito");
                }
                else MessageBox.Show("Horario no modificado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AceptarEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtIdDel.Text);
                horario = new Horario(short.Parse(txtIdDel.Text));
                implhorario = new HorarioImpl();
                int res = implhorario.Delete(horario);
                if (res > 0)
                {
                    MessageBox.Show("Horario eliminado con éxito");
                }
                else MessageBox.Show("Horario no eliminado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
