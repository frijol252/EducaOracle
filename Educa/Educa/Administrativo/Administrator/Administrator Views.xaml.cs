﻿using Microsoft.Maps.MapControl.WPF;
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
using Microsoft.Win32;

namespace Educa.Administrativo.Administrator
{
    /// <summary>
    /// Lógica de interacción para Administrator_Views.xaml
    /// </summary>
    public partial class Administrator_Views : UserControl
    {
        public Administrator_Views()
        {
            InitializeComponent();
        }

        Model.Administrator ad;
        AdministratorImpl administratorImpl;

        private void InsertNow_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //names, lastName, secondLastName, address, phone, birthDate,gender,startDate,email
                if (txtname.Text != "" && txtlastname.Text != "" && txtAddress.Text != "" && txtPhone.Text != "" && txtBirth.Text != "" && txtGender.Text != "" && txtemail.Text != "" && txtposition.Text != "" && txtprofesion.Text != "" && txtspeciality.Text !="")
                {
                    string second;
                    if (txtsecondlastname.Text == null)
                    {
                        second = "";
                    }
                    else
                    {
                        second = txtsecondlastname.Text;
                    }

                    ad = new Model.Administrator(txtname.Text, txtlastname.Text, txtsecondlastname.Text, txtAddress.Text, txtPhone.Text, txtBirth.SelectedDate.Value, txtGender.Text,txtstart.SelectedDate.Value, txtemail.Text,
                        ubicationPoint.Latitude, ubicationPoint.Longitude, byte.Parse(idTown.ToString()), pathImagePortada, txtposition.Text,txtprofesion.Text,txtspeciality.Text);

                    administratorImpl = new AdministratorImpl();
                    administratorImpl.InsertTransaction(ad);


                    image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(DBImplementation.pathImages + 0 + ".png");
                    image.EndInit();
                    imagesector.Source = image;



                    pathImagePortada = null;
                    MessageBox.Show("Administrator Inserted");
                }
                else
                {
                    MessageBox.Show("Don't leave data empty");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }
        BitmapImage image;
        string pathImagePortada;
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

        #region Combos

        int idTown = 0;
        CityImpl cityImpl;
        public void comboCity()
        {
            try
            {
                cityImpl = new CityImpl();
                DataTable ciudad = cityImpl.Select();
                comboCiudad.ItemsSource = ciudad.DefaultView;
                comboCiudad.DisplayMemberPath = "CityName";
                comboCiudad.SelectedValuePath = "CityId".ToString();
                comboCiudad.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }

        }
        string sValue = "";

        private void ComboCiudad_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView oDataRowView = comboCiudad.SelectedItem as DataRowView;


            if (oDataRowView != null)
            {
                sValue = oDataRowView.Row["CityName"] as string;
            }
            comboProvince();

        }
        ProvinceImpl provinceImpl;
        public void comboProvince()
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.Select(sValue);
            comboProvincia.ItemsSource = province.DefaultView;
            comboProvincia.DisplayMemberPath = "provinceName";
            comboProvincia.SelectedValuePath = "ProvinceId".ToString();
            comboProvincia.SelectedIndex = 0;
        }
        string sValuep = "";
        private void ComboProvincia_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView oDataRowView = comboProvincia.SelectedItem as DataRowView;


            if (oDataRowView != null)
            {
                sValuep = oDataRowView.Row["provinceName"] as string;
            }
            comboTown();
        }
        TownImpl townImpl;
        Town town;
        public void comboTown()
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.Select(sValue);
            comboMuni.ItemsSource = town.DefaultView;
            comboMuni.DisplayMemberPath = "name";
            comboMuni.SelectedValuePath = "id".ToString();
            comboMuni.SelectedIndex = 0;
        }
        string sValueT = "";
        

        private void ComboMuni_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView oDataRowView = comboMuni.SelectedItem as DataRowView;


            if (oDataRowView != null)
            {
                sValueT = oDataRowView.Row["name"] as string;
            }
            getIdTown();
        }
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








        #endregion

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            comboCity();
        }
    }
}
