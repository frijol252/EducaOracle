using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using DAO;
using Model;

namespace Implementation
{
    public class AdministratorImpl
    {
        User user;

        public void InsertTransaction(Administrator t)
        {
            string queryPerson = @"INSERT INTO Person (names, lastName, secondLastName, addres, phone, birthDate,gender,email,latitude,longitude,TownId,photo)
                            VALUES( :names, :lastName, :secondLastName, :address, :phone, :birthDate, :gender, :email, :latitude, :longitude, :TownId, :Photo)";
            string queryUser = @"INSERT INTO USERACCOUNT (USERNAME, PASSWORD, ROLE,PERSONID)
                            VALUES( :userName, STANDARD_HASH(:password, 'MD5'), :role, :PersonId);";

            string queryAdmin = @"INSERT INTO ADMINISTRATIVE(POSITION,PROFESSION,SPECIALITY,PERSONID)
                                VALUES( :position, :profesion, :speciality , :Personid)";
            


            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start student Insert.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(3);
                OracleParameter[] parameters1 = new OracleParameter[12];
                OracleParameter[] parameters2 = new OracleParameter[4];
                OracleParameter[] parameters3 = new OracleParameter[4];
                int id = DBImplementation.GetIdentityFromTable("Person");
                user = users(t.Names, t.LastName, id);
                cmds[0].CommandText = queryPerson;
                parameters1[0] = new OracleParameter(":names", id);
                parameters1[1] = new OracleParameter(":lastName", t.LastName);
                if (t.SecondLastName != null) parameters1[2] = new OracleParameter(":secondLastName", t.SecondLastName);
                else parameters1[2] = new OracleParameter(":secondLastName", DBNull.Value);
                parameters1[3] = new OracleParameter(":address", t.Address);
                parameters1[4] = new OracleParameter(":phone", t.Phone);
                parameters1[5] = new OracleParameter(":birthDate", t.BirthDate);
                parameters1[6] = new OracleParameter(":gender", t.Gender);
                parameters1[7] = new OracleParameter(":email", t.Email);
                parameters1[8] = new OracleParameter(":latitude", t.Latitude);
                parameters1[9] = new OracleParameter(":longitude", t.Longitude);
                parameters1[10] = new OracleParameter(":TownId", t.TownId);
                if (t.PathImage != null) { 

                    File.Copy(t.PathImage, DBImplementation.pathImages + id + ".png");
                    parameters1[11] = new OracleParameter(":Photo", id);
                    
                }
                else {
                    parameters1[11] = new OracleParameter(":Photo", "0");
                    
                }

                cmds[0].Parameters.AddRange(parameters1);

                cmds[1].CommandText = queryUser;
                parameters2[0] = new OracleParameter(":PersonId", id);
                parameters2[1] = new OracleParameter(":userName", user.UserName);
                parameters2[2] = new OracleParameter(":password", user.Password);
                parameters2[3] = new OracleParameter(":role", user.Role);
                cmds[1].Parameters.AddRange(parameters2);

                cmds[2].CommandText = queryAdmin;
                parameters3[0] = new OracleParameter(":Personid", id);
                parameters3[1] = new OracleParameter(":position", t.Position);
                parameters3[2] = new OracleParameter(":profesion", t.Profesion);
                parameters3[3] = new OracleParameter(":speciality", t.Speciality);
                cmds[2].Parameters.AddRange(parameters3);


                DBImplementation.ExecuteNBasicCommand(cmds);
                SendEmail(t.Email, user.UserName, user.Password);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Insert Admin by" + Session.SessionCurrent.ToString() + " Object Send: ({1})", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not insert Admin({1}).", DateTime.Now, ex.Message));
            }
        }

        Random rdn = new Random();
        public User users(string name, string last, int id)
        {
            User usuario;
            string username;
            string password;
            string role = "A";

            username = "" + name.Substring(0, 1).ToLower() + last.Substring(last.Length - 1, 1).ToLower() + id + last.Substring(0, 1).ToLower() + rdn.Next(100, 1000);

            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            password = "";

            for (int i = 0; i <= 4; i++)
            {
                password = password + caracteres.Substring(rdn.Next(1, 63), 1);
            }
            usuario = new User(username, password, role, id);

            return usuario;
        }
        private void SendEmail(string email, string username, string password)
        {


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
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Register Mail send whitout problems" + Session.SessionCurrent.ToString()));
            }


        }
    }
}
