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
using Microsoft.Maps.MapControl.WPF;
using System.Data;
using Microsoft.Win32;

namespace Educa.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para StudentModif.xaml
    /// </summary>
    public partial class StudentModif : Window
    {
        byte idmodi;
        BitmapImage image;
        Student stu;
        StudentImpl studentImpl;
        string pathImagePortada = null;
        string pathup = "";
        int id;
        int idPerson;
        public StudentModif(int id, int idPerson)
        {
            this.id = id;
            this.idPerson = idPerson;
            InitializeComponent();
        }
        #region controlgridup
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region aniadirDatos
        public void aniadirDatos()
        {
            studentImpl = new StudentImpl();
            stu = studentImpl.Get(id,idPerson);
            txtId.Text = stu.PersonId.ToString();
            txtname.Text = stu.Names;
            txtlastname.Text = stu.LastName;
            txtlastname.Text = stu.LastName;
            txtsecondlastname.Text = stu.SecondLastName;
            txtemail.Text = stu.Email;
            txtPhone.Text = stu.Phone;
            txtAddress.Text = stu.Address;

            image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(DBImplementation.pathImages + stu.Photo + ".png");
            image.EndInit();
            imagesector.Source = image;


            pathImagePortada = DBImplementation.pathImages + stu.Photo + ".png";
            Location ubi = new Location(stu.Latitude, stu.Longitude);
            MyMap.Center = ubi;
            ubicationPoint = ubi;
            Pushpin point = new Pushpin();
            point.Location = ubi;
            MyMap.Children.Clear();
            MyMap.Children.Add(point);
            idmodi = stu.TownId;
            llamarmuni(stu.TownId);
            txtrude.Text = stu.RudeNumber;
        }
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de Imagen|*.PNG";
            if (ofd.ShowDialog() == true)
            {
                image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(ofd.FileName);
                image.EndInit();
                imagesector.Source = image;
                pathImagePortada = ofd.FileName;
            }

        }
        #endregion
        #region mapa
        Location ubicationPoint;
        private void MyMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                e.Handled = true;
                var mousePosicion = e.GetPosition((UIElement)sender);
                ubicationPoint = MyMap.ViewportPointToLocation(mousePosicion);
                Pushpin point = new Pushpin();
                point.Location = ubicationPoint;
                MyMap.Children.Clear();
                MyMap.Children.Add(point);
                MessageBox.Show("Marked location");
            }
            catch
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }

        private void btnSaltelite_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();
            MyMap.Mode = new AerialMode(true);
        }

        private void btnCalles_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();
            MyMap.Mode = new RoadMode();
        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();
            MyMap.ZoomLevel++;
        }

        private void btnAlejar_Click(object sender, RoutedEventArgs e)
        {
            MyMap.ZoomLevel--;
        }
        #endregion
        #region combos

        CityImpl cityImpl;
        string sValue = "";
        ProvinceImpl provinceImpl;
        string sValuep = "";
        TownImpl townImpl;
        Town town;
        string sValueT = "";
        byte idTown;
        public void getIdTown()
        {
            try
            {

                townImpl = new TownImpl();
                town = townImpl.GetID(sValue, sValuep, sValueT);

                idTown = byte.Parse(town.Id.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }








        public void llamarprovince(int id)
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.SelectList(id);
            comboProvincia.ItemsSource = province.DefaultView;
            comboProvincia.DisplayMemberPath = "name";
            int count = 0;
            foreach (DataRow a in province.Rows)
            {

                int ids = int.Parse(a["id"].ToString());
                if (idprovince == ids)
                {
                    comboProvincia.SelectedIndex = count;
                    idcity = int.Parse(a["city"].ToString());
                    sValuep = a["name"].ToString();
                }
                count++;

            }
            llamarciudad(idcity);
        }

        private void ComboProvincia_DropDownClosed(object sender, EventArgs e)
        {
            provinceMod();
        }
        public void provinceMod()
        {
            if (comboProvincia.SelectedItem != null)
            {
                sValuep = comboProvincia.Text as string;
            }
            comboTownMod();
            muniMod();
        }

        public void comboTownMod()
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.Select(sValue);
            comboMuni.ItemsSource = town.DefaultView;
            comboMuni.DisplayMemberPath = "name";
            comboMuni.SelectedValuePath = "id".ToString();
            comboMuni.SelectedIndex = 0;
        }


        int idcity = -1;
        public void llamarciudad(int id)
        {
            cityImpl = new CityImpl();
            DataTable city = cityImpl.SelectList();
            comboCiudad.ItemsSource = city.DefaultView;
            comboCiudad.DisplayMemberPath = "name";
            int count = 0;
            foreach (DataRow row in city.Rows)
            {

                int ids = int.Parse(row["id"].ToString());
                if (idcity == ids)
                {
                    comboCiudad.SelectedIndex = count;
                    sValue = row["name"].ToString();
                }
                count++;

            }
            sValue = comboCiudad.Text;
        }



        private void ComboCiudad_DropDownClosed(object sender, EventArgs e)
        {
            ciudadMod();
        }
        public void ciudadMod()
        {
            if (comboCiudad.SelectedItem != null)
            {
                sValue = comboCiudad.Text as string;
            }
            comboProvinceMod();
            provinceMod();
        }

        public void comboProvinceMod()
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.Select(sValue);
            comboProvincia.ItemsSource = province.DefaultView;
            comboProvincia.DisplayMemberPath = "provinceName";
            comboProvincia.SelectedValuePath = "ProvinceId".ToString();
            comboProvincia.SelectedIndex = 0;
        }


        int idprovince = -1;
        public void llamarmuni(int id)
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.SelectList(id);
            comboMuni.ItemsSource = town.DefaultView;
            comboMuni.DisplayMemberPath = "name";
            int count = 0;
            foreach (DataRow row in town.Rows)
            {

                int ids = int.Parse(row["id"].ToString());
                if (idmodi == ids)
                {
                    comboMuni.SelectedIndex = count;
                    idTown = byte.Parse(idmodi.ToString());
                    sValueT = row["name"].ToString();
                }
                idprovince = int.Parse(row["province"].ToString());
                count++;
            }
            llamarprovince(idprovince);

        }
        private void ComboMuni_DropDownClosed(object sender, EventArgs e)
        {
            muniMod();
        }
        public void muniMod()
        {
            if (comboMuni.SelectedItem != null)
            {

                sValueT = comboMuni.Text;
            }

            getIdTown();
        }


        #endregion

        private void InsertNow_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(pathImagePortada);
                                     //string names, string lastName, string secondLastName, string address, string phone, string email, double latitude, double longitude, byte townId, string photo, string rudeNumber, int idCourse,string pathImage)
                stu = new Student(stu.PersonId, txtname.Text, txtlastname.Text, txtsecondlastname.Text, txtAddress.Text, txtPhone.Text, txtemail.Text, ubicationPoint.Latitude, ubicationPoint.Longitude, idTown, DBImplementation.pathImages+stu.Photo+".png", txtrude.Text,stu.IdCourse,pathImagePortada,stu.StudentId);
                studentImpl = new StudentImpl();
                studentImpl.UpdateTransaction(stu);

                MessageBox.Show("Teacher Modifed successfully!!!");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            aniadirDatos();
        }
    }
}
