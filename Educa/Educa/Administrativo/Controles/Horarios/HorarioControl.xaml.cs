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

namespace Educa.Controles.Horarios
{
    /// <summary>
    /// Lógica de interacción para HorarioControl.xaml
    /// </summary>
    public partial class HorarioControl : UserControl
    {
        public HorarioControl()
        {
            InitializeComponent();
        }

        private void AniadirHorario_Click(object sender, RoutedEventArgs e)
        {
            Educa.Controles.Horarios.HorarioAniadir win = new Educa.Controles.Horarios.HorarioAniadir();
            win.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Educa.Controles.Horarios.HorarioList win2 = new Educa.Controles.Horarios.HorarioList();
            win2.Show();
        }
    }
}
