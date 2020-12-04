using Microsoft.Win32;
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

namespace Educa.Administrativo.Controles.Account
{
    /// <summary>
    /// Lógica de interacción para ModifAccount.xaml
    /// </summary>
    public partial class ModifAccount : Window
    {
        public ModifAccount()
        {
            InitializeComponent();


            image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(DBImplementation.pathImages + Session.Sessionphoto + ".png");
            image.EndInit();
            imagesector.Source = image;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        Person person;
        PersonImpl personImpl=new PersonImpl();
        string photo;
        private void Window_Initialized(object sender, EventArgs e)
        {

            LoadDataGrid();
            
            
        }
        BitmapImage image;
        string pathImagePortada = DBImplementation.pathImages + Session.Sessionphoto + ".png";
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de Imagen|*.PNG";
            if (ofd.ShowDialog() == true)
            {
                image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofd.FileName);
                image.EndInit();
                imagesector.Source = image;
                pathImagePortada = ofd.FileName;
            }
        }
        public void LoadDataGrid()
        {
            person = personImpl.Get(16);
            photo = person.Photo;
            txtidperson.Text = person.PersonId.ToString();
            lblNames.Text = person.Names;
            txtlast.Text = person.LastName;
            txtSlast.Text = person.SecondLastName;
            txtphone.Text = person.Phone;
            lblmail.Text = person.Email;
            txtaddress.Text = person.Address;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (pathImagePortada != DBImplementation.pathImages + Session.Sessionphoto + ".png")
                {

                }
                person = new Person(int.Parse(txtidperson.Text), lblNames.Text, txtlast.Text, txtSlast.Text, txtaddress.Text, txtphone.Text, lblmail.Text, photo, pathImagePortada, (DBImplementation.pathImages + Session.Sessionphoto + ".png"));
                personImpl = new PersonImpl();
                int res = personImpl.Update(person);
                if (res > 0)
                {
                    MessageBox.Show("Teacher Modifed successfully!!!");
                    
                    Session.SessionCurrent = lblNames.Text + " " + txtlast.Text;
                    Session.Sessionphoto = photo;
                    Session.SessionEmail = lblmail.Text;
                    LoadDataGrid();
                }
                else MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
                AccountView mod = new AccountView();
                mod.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
