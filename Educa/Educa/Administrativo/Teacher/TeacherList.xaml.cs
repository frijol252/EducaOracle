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
using System.Data;
using Microsoft.Maps.MapControl.WPF;
using System.Net.Mail;
using Microsoft.Win32;
using System.IO;
using System.Windows.Automation.Peers;

namespace Educa.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherList.xaml
    /// </summary>
    public partial class TeacherList : Window
    {
        #region parameters
        string usuario;
        Model.Teacher teacher;
        TeacherImpl teacherimpl;
        User usuarioclass;
        UserImpl impluser;
        BitmapImage image;
        byte idTown = 1;
        #endregion;

        public TeacherList()
        {
            InitializeComponent();
            image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(DBImplementation.pathImages + 0 + ".png");
            image.EndInit();
            imagesector.Source = image;
            comboCity();


        }

        #region view;
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void modif()
        {
            Insestack.Height = 0;
            Modstack.Height = 500;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        Random ran=new Random();
        public void UsuarioAniadir()
        {
            if (txtsecondlastname.Text != null)
            {
                usuario = "" + txtlastname.Text.Substring(0, 1) + txtsecondlastname.Text.Substring(0, 1) + txtname.Text.Substring(0, 1) + DateTime.Now.ToString().Substring(6, 4) + ran.Next(100, 1000);
            }
            else
            {
                usuario = "" + txtlastname.Text.Substring(0, 1) + txtlastname.Text.Substring(txtlastname.MaxLength-1,1) + txtname.Text.Substring(0, 1) + DateTime.Now.ToString().Substring(6, 4) + ran.Next(100, 1000);
            }
            
        }
        Random rdn = new Random();
        public string contrasenia()
        {
            
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string contraseniaAleatoria="";
            for (int i=0; i<=4; i++)
            {
                contraseniaAleatoria = contraseniaAleatoria + caracteres.Substring(rdn.Next(1,63),1);
            }
            return contraseniaAleatoria;
        }

        private void InsertNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UsuarioAniadir();
                //names, lastName, secondLastName, address, phone, birthDate,gender,startDate,email
                if (txtname.Text != "" && txtlastname.Text != "" && txtAddress.Text != "" && txtPhone.Text != "" && txtBirth.Text != "" && txtGender.Text != "" && txtstartDate.Text != "" && txtemail.Text != "")
                {
                    string contraniafin = contrasenia();
                    string second;
                    if (txtsecondlastname.Text == null)
                    {
                        second = "";
                    }
                    else
                    {
                        second = txtsecondlastname.Text;
                    }
                    UsuarioAniadir();
                    teacher = new Model.Teacher(txtname.Text, txtlastname.Text, second, txtAddress.Text, txtPhone.Text, txtBirth.SelectedDate.Value, txtGender.Text, txtstartDate.SelectedDate.Value, txtemail.Text,ubicationPoint.Latitude,ubicationPoint.Longitude,idTown,pathImagePortada);
                    usuarioclass = new User(usuario,contraniafin,"P",0);

                    teacherimpl.InsertTransaction(usuarioclass, teacher);


                    image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(DBImplementation.pathImages + 0 + ".png");
                    image.EndInit();
                    imagesector.Source = image;



                    pathImagePortada = null;
                    SendEmail(txtemail.Text,usuario.ToUpper(), contraniafin);
                    LoadDataGrid();
                    ocultar();
                }
                else
                {
                    MessageBox.Show("Don't leave data empty");
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        private void SendEmail(string email, string username, string password)
        {
            #region mail
            
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(DBImplementation.usermail);
                    mail.To.Add(email);
                    mail.Subject = "Welcome to Educa";
                    mail.Body = "You were registered with the username: " + username + ", and with the password: " + password
                        + "\nOn your first login you will be asked for some parameters";

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(DBImplementation.usermail, DBImplementation.passwordmail);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                }
            
            #endregion
        }

        int idmodi=-1;
        string pathup="";
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    
                    modif();
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    teacherimpl = new TeacherImpl();
                    teacher = teacherimpl.Get(id);
                    txtid.Text = teacher.PersonId.ToString();
                    txtnameMod.Text = teacher.Names;
                    txtnameDel.Text = teacher.Names;
                    txtlastnameMod.Text = teacher.LastName;
                    txtlastnameDel.Text = teacher.LastName;
                    txtsecondlastnameMod.Text = teacher.SecondLastName;
                    txtemailMod.Text = teacher.Email;
                    txtPhoneMod.Text = teacher.Phone;
                    txtAddressMod.Text = teacher.Address;

                    image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(DBImplementation.pathImages + teacher.Photo + ".png");
                    image.EndInit();
                    imagesector.Source = image;


                    pathImagePortada = DBImplementation.pathImages + teacher.Photo+".png";
                    pathup = pathImagePortada;
                    Location ubi = new Location(teacher.Latitude, teacher.Longitude);
                    MyMap.Center = ubi;
                    ubicationPoint = ubi;
                    Pushpin point = new Pushpin();
                    point.Location = ubi;
                    MyMap.Children.Clear();
                    MyMap.Children.Add(point);
                    idmodi = teacher.TownId;
                    llamarmuni(teacher.TownId);
                    Modif.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                catch (Exception ex)
                {
                }
            }
        }
        

        private void Window_Initialized(object sender, EventArgs e)
        {
            
            
        }
        public void LoadDataGrid()
        {
            try
            {
                teacherimpl = new TeacherImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = teacherimpl.Select().DefaultView;

            }
            catch (Exception ex) {  }
        }
        private void ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
            dgvDatos.Columns[15].Visibility = Visibility.Hidden;
            dgvDatos.Columns[14].Visibility = Visibility.Hidden;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }
        
        private void Modifbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtid.Text);
                if (pathImagePortada!=pathup)
                {
                    
                }
                teacher = new Model.Teacher(id, txtnameMod.Text, txtlastnameMod.Text, txtsecondlastnameMod.Text,txtAddressMod.Text, txtPhoneMod.Text,txtemailMod.Text,ubicationPoint.Latitude,ubicationPoint.Longitude,idTown,pathup,pathImagePortada);
                teacherimpl = new TeacherImpl();
                int res = teacherimpl.Update(teacher);
                if (res > 0)
                {
                    MessageBox.Show("Teacher Modifed successfully!!!");
                    LoadDataGrid();
                    ocultar();

                }
                else MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
            catch (Exception ex)
            {
            }

        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtid.Text);
                teacher = new Model.Teacher(id,txtnameDel.Text,txtlastnameDel.Text);
                teacherimpl = new TeacherImpl();
                teacherimpl.Delete(teacher);

                LoadDataGrid();
                ocultar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }

        string pathImagePortada = null;

        private void btnAddImage_Click(object sender, RoutedEventArgs e) {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Archivos de Imagen|*.PNG";
                if (ofd.ShowDialog() == true) {
                image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(ofd.FileName);
                image.EndInit();
                imagesector.Source = image;
                    pathImagePortada = ofd.FileName;
                }

            }

        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    teacherimpl = new TeacherImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = teacherimpl.Select().DefaultView;
                    ocultar();
                }
                else
                {
                    teacherimpl = new TeacherImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = teacherimpl.SelectSearch(txtsearch.Text).DefaultView;
                    ocultar();
                }
            }
            catch(Exception ex)
            {
            }
            
        }
        #endregion;

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
        CityImpl cityImpl;
        string sValue = "";
        ProvinceImpl provinceImpl;
        string sValuep = "";
        TownImpl townImpl;
        Town town;
        string sValueT = "";
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
       

        private void ComboCiudad_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView oDataRowView = comboCiudad.SelectedItem as DataRowView;


            if (oDataRowView != null)
            {
                sValue = oDataRowView.Row["CityName"] as string;
            }
            comboProvince();

        }
        
        public void comboProvince()
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.Select(sValue);
            comboProvincia.ItemsSource = province.DefaultView;
            comboProvincia.DisplayMemberPath = "provinceName";
            comboProvincia.SelectedValuePath = "ProvinceId".ToString();
            comboProvincia.SelectedIndex = 0;
        }
        
        private void ComboProvincia_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView oDataRowView = comboProvincia.SelectedItem as DataRowView;


            if (oDataRowView != null)
            {
                sValuep = oDataRowView.Row["provinceName"] as string;
            }
            comboTown();
        }
        
        public void comboTown()
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.Select(sValue);
            comboMuni.ItemsSource = town.DefaultView;
            comboMuni.DisplayMemberPath = "name";
            comboMuni.SelectedValuePath = "id".ToString();
            comboMuni.SelectedIndex = 0;
        }
        
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
                town = townImpl.GetID(sValue, sValuep,sValueT);
                
                idTown = byte.Parse(town.Id.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }








        public void llamarprovince(int id)
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.SelectList(id);
            comboProvinciaMod.ItemsSource = province.DefaultView;
            comboProvinciaMod.DisplayMemberPath = "name";
            int count = 0;
            foreach (DataRow a in province.Rows)
            {

                int ids = int.Parse(a["id"].ToString());
                if (idprovince == ids)
                {
                    comboProvinciaMod.SelectedIndex = count;
                    idcity = int.Parse(a["city"].ToString());
                    sValuep = a["name"].ToString();
                }
                count++;
                
            }
            llamarciudad(idcity);
        }

        private void ComboProvinciaMod_DropDownClosed(object sender, EventArgs e)
        {
            provinceMod();
        }
        public void provinceMod()
        {
            if (comboProvinciaMod.SelectedItem != null)
            {
                sValuep = comboProvinciaMod.Text as string;
            }
            comboTownMod();
            muniMod();
        }

        public void comboTownMod()
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.Select(sValue);
            comboMuniMod.ItemsSource = town.DefaultView;
            comboMuniMod.DisplayMemberPath = "name";
            comboMuniMod.SelectedValuePath = "id".ToString();
            comboMuniMod.SelectedIndex = 0;
        }


        int idcity = -1;
        public void llamarciudad(int id)
        {
            cityImpl = new CityImpl();
            DataTable city = cityImpl.SelectList();
            comboCiudadMod.ItemsSource = city.DefaultView;
            comboCiudadMod.DisplayMemberPath = "name";
            int count = 0;
            foreach (DataRow row in city.Rows)
            {

                int ids = int.Parse(row["id"].ToString());
                if (idcity == ids)
                {
                    comboCiudadMod.SelectedIndex = count;
                    sValue = row["name"].ToString();
                }
                count++;

            }
            sValue = comboCiudadMod.Text;
        }



        private void ComboCiudadMod_DropDownClosed(object sender, EventArgs e)
        {
            ciudadMod();
        }
        public void ciudadMod()
        {
            if (comboCiudadMod.SelectedItem != null)
            {
                sValue = comboCiudadMod.Text as string;
            }
            comboProvinceMod();
            provinceMod();
        }

        public void comboProvinceMod()
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.Select(sValue);
            comboProvinciaMod.ItemsSource = province.DefaultView;
            comboProvinciaMod.DisplayMemberPath = "provinceName";
            comboProvinciaMod.SelectedValuePath = "ProvinceId".ToString();
            comboProvinciaMod.SelectedIndex = 0;
        }


        int idprovince = -1;
        public void llamarmuni(int id)
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.SelectList(id);
            comboMuniMod.ItemsSource = town.DefaultView;
            comboMuniMod.DisplayMemberPath = "name";
            int count = 0;
            foreach (DataRow row in town.Rows)
            {

                int ids = int.Parse(row["id"].ToString());
                if (idmodi == ids)
                {
                    comboMuniMod.SelectedIndex = count;
                    idTown = byte.Parse(idmodi.ToString());
                    sValueT = row["name"].ToString();
                }
                idprovince = int.Parse(row["province"].ToString());
                count++;
            }
            llamarprovince(idprovince);

        }
        private void ComboMuniMod_DropDownClosed(object sender, EventArgs e)
        {
            muniMod();
        }
        public void muniMod()
        {
            if (comboMuniMod.SelectedItem != null)
            {
                
                sValueT = comboMuniMod.Text ;
            }

            getIdTown();
        }



        #endregion

        private void dgvDatos_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
            ocultar();
            comboCity();
        }
    }
}
