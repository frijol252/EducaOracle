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

namespace Educa
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        UserImpl implUser;
        private bool accept=false;
        private bool revisara = false;
        
        public MainWindow()
        {
            InitializeComponent();
           // System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Inicio del Método Update para Mall.", DateTime.Now));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //boton cerrar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
        
        private void Button_Log(object sender, RoutedEventArgs e)
        {
            
            
            
            try
            {
                //Llamamos al Menu
                if (accept == true)
                {
                    
                        if (revisara.Equals(false))
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("{0} | Log Failed: ", DateTime.Now));
                            MessageBox.Show("For your security please change your password");
                            Educa.passwordControl.PasswordLog i = new passwordControl.PasswordLog();
                            i.Show();
                            this.Visibility = Visibility.Hidden;
                            hecho.IsOpen = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("{0} | Log:  UserLog({1}).", DateTime.Now, Session.SessionUser));
                            switch (Session.SessionRole)
                            {

                                case ("P"):

                                    Educa.TeacherViews.Home.TeacherHome P = new TeacherViews.Home.TeacherHome();
                                    P.Show();
                                    this.Visibility = Visibility.Hidden;
                                    hecho.IsOpen = false;
                                    break;
                                case ("A"):
                                    Educa.Index i = new Educa.Index();
                                    i.Show();
                                    this.Visibility = Visibility.Hidden;
                                    hecho.IsOpen = false;
                                    break;
                                case ("D"):
                                    Educa.Index J = new Educa.Index();
                                    J.Show();
                                    this.Visibility = Visibility.Hidden;
                                    hecho.IsOpen = false;
                                    break;
                                case ("E"):
                                        StudentsView.Home.IndexStudent es = new StudentsView.Home.IndexStudent();
                                        es.Show();
                                        this.Visibility = Visibility.Hidden;
                                        hecho.IsOpen = false;
                                        break;
                            }
                        }
                    
                    
                }
                else
                {
                    hecho.IsOpen = false;
                }
                

                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com ");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtuser.Text != "" || txtpass.Password != "")
                {
                    implUser = new UserImpl();
                    DataTable dt = implUser.Login(txtuser.Text.ToLower(), txtpass.Password);
                    if (dt.Rows.Count > 0)
                    {
                        //Existe el usuario
                        //Los variables de sesión
                        Session.SessionID = int.Parse(dt.Rows[0][0].ToString());
                        Session.SessionUser = dt.Rows[0][1].ToString();
                        Session.SessionRole = dt.Rows[0][3].ToString();
                        Session.SessionCurrent = dt.Rows[0][4].ToString();
                        Session.SessionEmail = dt.Rows[0][6].ToString();
                        Session.Sessionphoto= dt.Rows[0][7].ToString();
                        Session.Sessionstat= dt.Rows[0][8].ToString();
                        txtmensaje.Text = "Welcome "+dt.Rows[0][4].ToString();
                        accept = true;
                        if (int.Parse(dt.Rows[0][5].ToString())>0)
                        {
                            revisara = true;
                            if (int.Parse(Session.Sessionstat.ToString()) == 0)
                            {
                                MessageBox.Show("Your account is disable \n Please communicate with administration");
                            }
                            else
                            {
                                hecho.IsOpen = true;
                            }
                        }
                        else
                        {
                            revisara = false;
                        }
                        if (int.Parse(Session.Sessionstat.ToString()) == 0)
                {
                    MessageBox.Show("Your account is disable \n Please communicate with administration");
                }
                else
                {
                    hecho.IsOpen = true;
                }
                    }
                    else
                    {

                        txtmensaje.Text = "User name or password is incorrect";
                        accept = false;
                    }
                }
                else
                {
                    txtmensaje.Text = "Don't leave any blank space";
                    accept = false;
                }
                
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("User name or password is incorrect");
            }
            
            
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Educa.passwordControl.ForgotPassword fp = new passwordControl.ForgotPassword();
            fp.Show();
            this.Close();
        }
    }
}
