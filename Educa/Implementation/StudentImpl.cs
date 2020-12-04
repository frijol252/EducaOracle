using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAO;
using System.Data;
using System.Net.Mail;
using System.IO;
using Oracle.DataAccess.Client;
namespace Implementation
{
    public class StudentImpl
    {
        #region classes_region
        User user;
        #endregion
        #region Crud
        //GET
        public Student Get(int id,int person)
        {
            Student a = null;
            OracleCommand cmd = null;
            string query = @"SELECT P.Personid
      ,P.names ,P.lastName
      ,P.secondLastName ,S.rudeNumber,P.email,P.gender,P.phone ,P.addres   ,P.birthDate ,
	  P.status , P.registrationDate
      ,(NVL(P.updateDate,'1993-01-01')) ,(NVL(P.startDate,'1993-01-01')),(NVL(P.finishDate,'1993-01-01')) ,P.latitude,P.longitude,P.TownId,P.photo ,
	  S.studentId
	  FROM Person P left JOIN Student S ON S.PersonId=P.PersonId WHERE S.idCourse= :Course AND P.PersonId= :PersonId";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":Course", id);
                parameters1[1] = new OracleParameter(":PersonId", person);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {

                    a = new Student(int.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[8].ToString(), dr[7].ToString(),
                        DateTime.Parse(dr[9].ToString()), dr[6].ToString(),byte.Parse(dr[10].ToString()),DateTime.Parse(dr[11].ToString()),
                        DateTime.Parse(dr[12].ToString()),DateTime.Parse(dr[13].ToString()),DateTime.Parse(dr[14].ToString()), dr[5].ToString(),
                        Convert.ToDouble(dr[15].ToString()),Convert.ToDouble(dr[16].ToString()),byte.Parse(dr[17].ToString()), dr[18].ToString(),
                        dr[4].ToString(),int.Parse(dr[19].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                dr.Close();
            }
            return a;
        }





        //SELECT
        public DataTable Select(int id)
        {

            string query = @"SELECT P.Personid
      , P.names ,P.lastName
      ,P.secondLastName ,S.rudeNumber,P.email,P.gender,P.phone ,P.addres ,P.birthDate ,
	  P.status , P.registrationDate
      ,P.updateDate ,P.startDate,P.finishDate,(SELECT C.CityName||'-'||PP.provinceName||', '||T.townName
	  FROM Town T INNER JOIN Province PP ON PP.ProvinceId=T.ProvinceId
	  INNER JOIN City C ON C.CityId=PP.CityId WHERE T.TownId=P.TownId),P.photo ,
	  S.studentId
      FROM Person P left JOIN Student S ON S.PersonId = P.PersonId WHERE S.idCourse = :Course AND P.status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Course", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable SelectDis(int id)
        {

            string query = @"SELECT P.Personid
      , P.names ,P.lastName
      ,P.secondLastName ,S.rudeNumber,P.email,P.gender,P.phone ,P.addres   ,P.birthDate ,
	  P.status , P.registrationDate
      ,P.updateDate ,P.startDate,P.finishDate,(SELECT C.CityName||'-'||PP.provinceName||', '||T.townName
	  FROM Town T INNER JOIN Province PP ON PP.ProvinceId=T.ProvinceId
	  INNER JOIN City C ON C.CityId=PP.CityId WHERE T.TownId=P.TownId),P.photo ,
	  S.studentId
      FROM Person P left JOIN Student S ON S.PersonId = P.PersonId WHERE S.idCourse = :Course AND P.status=0";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Course", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable Selectlike(int id,string like)
        {

            string query = @"SELECT P.Personid
      , P.names ,P.lastName
      ,P.secondLastName ,S.rudeNumber,P.email,P.gender,P.phone ,P.addres   ,P.birthDate ,
	  P.status , P.registrationDate
      ,P.updateDate ,P.startDate,P.finishDate,(SELECT C.CityName||'-'||PP.provinceName||', '||T.townName
	  FROM Town T INNER JOIN Province PP ON PP.ProvinceId=T.ProvinceId
	  INNER JOIN City C ON C.CityId=PP.CityId WHERE T.TownId=P.TownId),P.photo ,
	  S.studentId
      FROM Person P left JOIN Student S ON S.PersonId = P.PersonId WHERE S.idCourse = :Course AND P.names||' '||P.lastName||' '||P.secondLastName like :like
 AND P.status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":Course", id);
                parameters1[1] = new OracleParameter(":like", "%" + like + "%");
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        public DataTable SelectDislike(int id, string like)
        {

            string query = @"SELECT P.Personid
      , P.names ,P.lastName
      ,P.secondLastName ,S.rudeNumber,P.email,P.gender,P.phone ,P.addres   ,P.birthDate ,
	  P.status , P.registrationDate
      ,P.updateDate ,P.startDate,P.finishDate,(SELECT C.CityName||'-'||PP.provinceName||', '||T.townName
	  FROM Town T INNER JOIN Province PP ON PP.ProvinceId=T.ProvinceId
	  INNER JOIN City C ON C.CityId=PP.CityId WHERE T.TownId=P.TownId),P.photo ,
	  S.studentId
      FROM Person P left JOIN Student S ON S.PersonId = P.PersonId WHERE S.idCourse = :Course AND P.names||' '||P.lastName||' '||P.secondLastName like :like
 AND P.status=0";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":Course", id);
                parameters1[1] = new OracleParameter(":like", "%" + like + "%");
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        #region Insert
        public void InsertTransaction(Student t)
        {
            string queryPerson = @"INSERT INTO Person (names, lastName, secondLastName, 
                                addres, phone, birthDate,gender,email,
                                latitude,longitude,TownId,photo)
                                VALUES( :names, :lastName, :secondLastName, :address,
                                :phone, :birthDate, :gender, :email , :latitude, :longitude, :TownId, :Photo)";

            string queryUser = @"INSERT INTO Users (userName, password, role,Personid)
                            VALUES( :userName, standard_hash(:password, 'MD5'), :role, :PersonId)";

            string queryStudent = @"INSERT INTO Student(rudeNumber,PersonId,idCourse)
	  VALUES( :rude, :Personid, :Course)";


            string queryAve = @"INSERT INTO AverageGradeTotal(firstTrimester)
  VALUES(0) ";

            string queryGrades = @"INSERT INTO Grade(studentid,idClass,idAverage)
  VALUES( :student, :class, :average) ";

            string queryFirst = @"INSERT INTO FirstTrimester(gradeId)
  VALUES( :id)";

            string querySecond = @"INSERT INTO SecondTrimester(gradeId)
  VALUES( :id)";

            string queryThird = @"INSERT INTO ThirdTrimester(gradeId)
  VALUES( :id)";


            try
            {
                int subjects = 0;
                DataTable dt = new DataTable();
                dt = SelectSubjects(t.IdCourse);
                foreach (DataRow d in dt.Rows)
                {
                    subjects++;
                }
                int i = 3;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start student Insert.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(3+(subjects*7));
                List<OracleParameter[]> parameters = new List<OracleParameter[]>();


                int id = DBImplementation.GetIdentityFromTable("Person");
                user = users(t.Names, t.LastName, id);

                cmds[0].CommandText = queryPerson;
                parameters.Add(new OracleParameter[12]);
                
                
                

                parameters[0][0] = new OracleParameter(":names", t.Names);
                parameters[0][1] = new OracleParameter(":lastName", t.LastName);
                if (t.SecondLastName != null) parameters[0][1] = new OracleParameter(":secondLastName", t.SecondLastName); 
                else parameters[0][1] = new OracleParameter(":secondLastName", DBNull.Value);
                parameters[0][2] = new OracleParameter(":address", t.Address);
                parameters[0][3] = new OracleParameter(":phone", t.Phone);
                parameters[0][4] = new OracleParameter(":birthDate", t.BirthDate);
                parameters[0][5] = new OracleParameter(":gender", t.Gender);
                parameters[0][6] = new OracleParameter(":email", t.Email);
                parameters[0][7] = new OracleParameter(":latitude", t.Latitude);
                parameters[0][8] = new OracleParameter(":longitude", t.Longitude);
                parameters[0][9] = new OracleParameter(":TownId", t.TownId);
                if (t.PathImage != null)
                {
                    File.Copy(t.PathImage, DBImplementation.pathImages + id + ".png");
                    parameters[0][10] = new OracleParameter(":Photo", id);
                }
                else { parameters[0][11] = new OracleParameter(":Photo", "0");  }
                cmds[0].Parameters.AddRange(parameters[0]);

                cmds[1].CommandText = queryUser;
                parameters.Add(new OracleParameter[4]);
                parameters[1][0] = new OracleParameter(":PersonId", id);
                parameters[1][1] = new OracleParameter(":userName", user.UserName);
                parameters[1][2] = new OracleParameter(":password", user.Password);
                parameters[1][3] = new OracleParameter(":role", user.Role);
                cmds[1].Parameters.AddRange(parameters[1]);

                cmds[2].CommandText = queryStudent;
                parameters.Add(new OracleParameter[3]);
                parameters[2][0] = new OracleParameter(":PersonId", id);
                parameters[2][1] = new OracleParameter(":rude", t.RudeNumber);
                parameters[2][2] = new OracleParameter(":Course", t.IdCourse);
                cmds[2].Parameters.AddRange(parameters[2]);

                int idstudent = DBImplementation.GetIdentityFromTable("Student");
                int idgradetotal = DBImplementation.GetIdentityFromTable("Grade");
                int idgradeincrement = DBImplementation.GetIncementFromTable("Grade");
                int idaveragetotal = DBImplementation.GetIdentityFromTable("AverageGradeTotal");
                int idaverageincrement = DBImplementation.GetIncementFromTable("AverageGradeTotal");
                int idaverage = idaveragetotal - idaverageincrement;
                int idgrade = idgradetotal - idgradeincrement;
               
                foreach (DataRow d in dt.Rows)
                {
                    idaverage = idaverage + idaverageincrement;
                    cmds[i].CommandText = queryAve;
                    parameters.Add(new OracleParameter[0]);
                    i++;
                    for (int k = 0; k < 3; k++)
                    {

                        switch (k)
                        {
                            case 0:
                                idgrade = idgrade + idgradeincrement;
                                cmds[i].CommandText = queryGrades;
                                parameters.Add(new OracleParameter[3]);
                                parameters[i][0] = new OracleParameter(":student", idstudent);
                                parameters[i][1] = new OracleParameter(":class", id);
                                parameters[i][2] = new OracleParameter(":average", d[0].ToString());
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                cmds[i].CommandText = queryFirst;
                                parameters.Add(new OracleParameter[1]);
                                parameters[i][0] = new OracleParameter(":id", idgrade);
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                break;
                            case 1:
                                idgrade = idgrade + idgradeincrement;
                                cmds[i].CommandText = queryGrades;
                                parameters.Add(new OracleParameter[3]);
                                parameters[i][0] = new OracleParameter(":student", idstudent);
                                parameters[i][1] = new OracleParameter(":class", id);
                                parameters[i][2] = new OracleParameter(":average", d[0].ToString());
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                cmds[i].CommandText = querySecond;
                                parameters.Add(new OracleParameter[1]);
                                parameters[i][0] = new OracleParameter(":id", idgrade);
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                break;
                            case 2:
                                idgrade = idgrade + idgradeincrement;
                                cmds[i].CommandText = queryGrades;
                                parameters.Add(new OracleParameter[3]);
                                parameters[i][0] = new OracleParameter(":student", idstudent);
                                parameters[i][1] = new OracleParameter(":class", id);
                                parameters[i][2] = new OracleParameter(":average", d[0].ToString());
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                cmds[i].CommandText = queryThird;
                                parameters.Add(new OracleParameter[1]);
                                parameters[i][0] = new OracleParameter(":id", idgrade);
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                
                                break;
                        }
                    }
                }
                
                DBImplementation.ExecuteNBasicCommand(cmds);
                SendEmail(t.Email, user.UserName, user.Password);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Insert student by" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not insert Student({1}).", DateTime.Now, ex.Message));
            }
        }


        public DataTable SelectSubjects(int id)
        {

            string query = @"SELECT C.idClass FROM Class C 
  WHERE C.idCourse= :Course AND C.status>0 ";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Course", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }


        Random rdn = new Random();
        public User users(string name, string last,int id)
        {
            User usuario;
                string username;
                string password;
                string role = "E";

                username = "" + name.Substring(0, 1).ToLower() + last.Substring(last.Length - 1, 1).ToLower() +id + last.Substring(0, 1).ToLower() + rdn.Next(100, 1000);
                
                string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                password = "";

                for (int i = 0; i <= 4; i++)
                {
                    password = password + caracteres.Substring(rdn.Next(1, 63), 1);
                }
                usuario = new User(username, password, role, id);
            
            return usuario;
        }
        //END INSERT

        //mail
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
        #endregion


        //UPDATE
        public void UpdateTransaction(Student t)
        {
            string queryPerson = @"UPDATE Person SET names=
                                    :names, lastName= :lastName, secondLastName= :secondLastName, 
                                    addres= :address, phone= :phone,email= :email,latitude= :latitude,
                                    longitude= :longitude,TownId= :TownId,photo= :photo WHERE Personid= :Person
                                    ";
            

            string queryStudent = @"UPDATE Student SET rudeNumber= :rude WHERE studentId
  =(SELECT S.studentId FROM Student S INNER JOIN Person P ON P.Personid=S.PersonId WHERE P.Personid= :Person)";



            try
            {

                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start student Update.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                OracleParameter[] parameters1 = new OracleParameter[11];
                OracleParameter[] parameters2 = new OracleParameter[2];

                int id = DBImplementation.GetIdentityFromTable("Person");
                user = users(t.Names, t.LastName, id);
                cmds[0].CommandText = queryPerson;
                parameters1[0] = new OracleParameter(":Person", t.PersonId);
                parameters1[1] = new OracleParameter(":names", t.Names);
                parameters1[2] = new OracleParameter(":lastName", t.LastName);
                if (t.SecondLastName != null) parameters1[3] = new OracleParameter(":secondLastName", t.SecondLastName); 
                else parameters1[3] = new OracleParameter(":secondLastName", DBNull.Value); 
                parameters1[4] = new OracleParameter(":address", t.Address);
                parameters1[5] = new OracleParameter(":phone", t.Phone);
                parameters1[6] = new OracleParameter(":email", t.Email);
                parameters1[7] = new OracleParameter(":latitude", t.Latitude);
                parameters1[8] = new OracleParameter(":longitude", t.Longitude);
                parameters1[9] = new OracleParameter(":TownId", t.TownId);
                
                if (t.Photo == t.PathImage)
                {
                    parameters1[10] = new OracleParameter(":photo", t.PersonId);
                }
                else
                {
                    if (t.PathImage==DBImplementation.pathImages+"0.png")
                    {
                        parameters1[10] = new OracleParameter(":photo", 0);
                    }
                    else
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(DBImplementation.pathImages + t.PersonId + ".png");
                        File.Copy(t.PathImage, DBImplementation.pathImages + t.PersonId + ".png");
                        parameters1[0] = new OracleParameter(":photo", t.PersonId);
                    }
                }
                cmds[0].Parameters.AddRange(parameters1);

                cmds[1].CommandText = queryStudent;
                parameters2[0] = new OracleParameter(":rude", t.RudeNumber);
                parameters2[1] = new OracleParameter(":Person", t.PersonId);
                cmds[1].Parameters.AddRange(parameters2);

                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Modif student by" + Session.SessionCurrent.ToString()+"/" + DateTime.Now));
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not modif Student({1}).", DateTime.Now, ex.Message));
            }
        }
        public void UpdateEnabled(int id, int stat)
        {
            string queryPerson = @"UPDATE Person SET status= :stat WHERE Personid= :Person";


            string queryStudent = @"UPDATE Users SET status= :stat WHERE Personid= :Person";



            try
            {

                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start student Update.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                OracleParameter[] parameters1 = new OracleParameter[1];
                OracleParameter[] parameters2 = new OracleParameter[1];
                cmds[0].CommandText = queryPerson;
                parameters1[0] = new OracleParameter(":Person", id);
                parameters1[1] = new OracleParameter(":stat", stat);
                cmds[0].Parameters.AddRange(parameters1);

                cmds[1].CommandText = queryStudent;
                parameters2[0] = new OracleParameter(":stat", stat);
                parameters2[1] = new OracleParameter(":Person", id);
                cmds[1].Parameters.AddRange(parameters2);


                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Modif student by" + Session.SessionCurrent.ToString() + "/" + DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not modif Student({1}).", DateTime.Now, ex.Message));
            }
        }
        public void DeleteTransaction(int t)
        {
            string queryPerson = @"UPDATE Person SET status=2 WHERE PersonId= :Person";
            string queryUser = @"UPDATE Users SET status=2 WHERE PersonId= :Person";



            try
            {

                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start student Update.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                OracleParameter[] parameters1 = new OracleParameter[1];
                OracleParameter[] parameters2 = new OracleParameter[1];
                cmds[0].CommandText = queryPerson;
                parameters1[0] = new OracleParameter(":Person", t);
                cmds[0].Parameters.AddRange(parameters1);

                cmds[1].CommandText = queryUser;
                parameters2[0] = new OracleParameter(":Person", t);
                cmds[1].Parameters.AddRange(parameters2);


                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Delete student by" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not delete Student({1}).", DateTime.Now, ex.Message));
            }
        }

        #endregion
    }
}
