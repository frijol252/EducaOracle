using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAO;
using System.Data;
using Oracle.DataAccess.Client;
using System.IO;

namespace Implementation
{
    public class TeacherImpl
    {

        

        public void InsertTransaction(User u, Teacher t)
        {
            string queryPerson = @"INSERT INTO Person (names, lastName, secondLastName, 
                                addres, phone, birthDate,gender,startDate,email,latitude,longitude,TownId,photo)
                            VALUES( :names, :lastName, :secondLastName, :address, :phone, 
                            :birthDate, :gender, :startDate, :email , :latitude, :longitude, :TownId, :Photo)";

            string queryUser = @"INSERT INTO Users (userName, password, role,Personid)
                            VALUES( :userName, standard_hash(:password, 'MD5'), :role, :PersonId)";

            string queryTeacher = @"INSERT INTO Teacher (Personid)
                            VALUES ( :Personid)";
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start teacher Insert.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(3);
                OracleParameter[] parameters1 = new OracleParameter[13];
                OracleParameter[] parameters2 = new OracleParameter[4];
                OracleParameter[] parameters3 = new OracleParameter[1];
                int id = DBImplementation.GetIdentityFromTable("Person");
                cmds[0].CommandText = queryPerson;
                parameters1[0] = new OracleParameter(":names", t.Names);
                parameters1[1] = new OracleParameter(":lastName", t.LastName);
                if (t.SecondLastName != null) parameters1[2] = new OracleParameter(":secondLastName", t.SecondLastName); 
                else parameters1[2] = new OracleParameter(":secondLastName", DBNull.Value); 
                parameters1[3] = new OracleParameter(":address", t.Address);
                parameters1[4] = new OracleParameter(":phone", t.Phone);
                parameters1[5] = new OracleParameter(":birthDate", t.BirthDate);
                parameters1[6] = new OracleParameter(":gender", t.Gender);
                parameters1[7] = new OracleParameter(":startDate", t.StartDate);
                parameters1[8] = new OracleParameter(":email", t.Email);
                parameters1[9] = new OracleParameter(":latitude", t.Latitude);
                parameters1[10] = new OracleParameter(":longitude", t.Longitude);
                parameters1[11] = new OracleParameter(":TownId", t.TownId);
                if (t.PathImage != null)
                {
                    File.Copy(t.PathImage, DBImplementation.pathImages + id + ".png");
                    parameters1[12] = new OracleParameter(":Photo", id);
                }
                else { parameters1[12] = new OracleParameter(":Photo", "0"); }

                cmds[0].Parameters.AddRange(parameters1);

                cmds[1].CommandText = queryUser;
                parameters2[0] = new OracleParameter(":PersonId", id);
                parameters2[1] = new OracleParameter(":userName", u.UserName);
                parameters2[2] = new OracleParameter(":password", u.Password);
                parameters2[3] = new OracleParameter(":role", t.Names);
                cmds[1].Parameters.AddRange(parameters2);

                cmds[2].CommandText = queryTeacher;
                parameters3[0] = new OracleParameter(":Personid", id);
                cmds[2].Parameters.AddRange(parameters2);

                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Insert teacher by"+Session.SessionCurrent.ToString()+" Objeto enviado: {1} por el usuario #{2}.", DateTime.Now, u.UserName));
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not insert Teacher({1}). Objeto: {2}.", DateTime.Now, ex.Message, u.UserName));
            }
        }
        
        
        //Update
        public int Update(Teacher t)
        {
            string query = @"UPDATE Person SET names= :names, 
                            lastName= :lastName, secondLastName= :secondLastName, 
                            addres= :address, phone= :phone, email= :email,latitude= :latitude,
                            longitude= :longitude, TownId= :town, photo= :photo, 
                            updateDate=CURRENT_TIMESTAMP WHERE Personid= :PersonId";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[11];

                parameters1[0] = new OracleParameter(":PersonId", t.PersonId);
                parameters1[1] = new OracleParameter(":names", t.Names);
                parameters1[2] = new OracleParameter(":lastName", t.LastName);
                parameters1[3] = new OracleParameter(":secondLastName", t.SecondLastName);
                parameters1[4] = new OracleParameter(":address", t.Address);
                parameters1[5] = new OracleParameter(":phone", t.Phone);
                parameters1[6] = new OracleParameter(":email", t.Email);
                parameters1[7] = new OracleParameter(":latitude", t.Latitude);
                parameters1[8] = new OracleParameter(":userName", t.Longitude);
                parameters1[9] = new OracleParameter(":town", t.TownId);
                if (t.Pathrevision==t.PathImage)
                {
                    parameters1[10] = new OracleParameter(":photo", t.PersonId);  }
                else
                {
                    parameters1[10] = new OracleParameter(":photo", t.PersonId); 
                    
                    File.Delete(t.Pathrevision);
                    File.Copy(t.PathImage, DBImplementation.pathImages + t.PersonId + ".png");
                }
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteBasicCommand(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Disabled
        public int Disabled(Teacher t)
        {
            string query = @"UPDATE Person SET status=0 WHERE Personid= :PersonId";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":PersonId", t.PersonId);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteBasicCommand(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Enabled
        public int Enabled(Teacher t)
        {
            string query = @"UPDATE Person SET status=1 WHERE Personid= :PersonId";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":PersonId", t.PersonId);
                cmd.Parameters.AddRange(parameters1);

                return DBImplementation.ExecuteBasicCommand(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DELETE
        public void Delete(Teacher t)
        {
            
            string queryUser = @"DELETE Users WHERE PersonId= :PersonId";

            string queryTeacher = @"DELETE Teacher WHERE PersonId= :PersonId";

            string queryPerson = @"DELETE Person WHERE PersonId= :PersonId";
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Delete teacher Insert.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(3);
                OracleParameter[] parameters1 = new OracleParameter[1];
                OracleParameter[] parameters2 = new OracleParameter[1];
                OracleParameter[] parameters3 = new OracleParameter[1];

                cmds[0].CommandText = queryTeacher;
                parameters1[0] = new OracleParameter(":PersonId", t.PersonId);
                cmds[0].Parameters.AddRange(parameters1);

                cmds[1].CommandText = queryUser;
                parameters2[0] = new OracleParameter(":PersonId", t.PersonId);
                cmds[1].Parameters.AddRange(parameters2);

                cmds[2].CommandText = queryPerson;
                parameters3[0] = new OracleParameter(":PersonId", t.PersonId);
                cmds[2].Parameters.AddRange(parameters3);


                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Delete Teacher by"+Session.SessionCurrent.ToString() +". Objeto enviado: {1} por el usuario #{2}.", DateTime.Now, t.Names+" "+t.LastName));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not delete Teacher({1}). Objeto: {2}.", DateTime.Now, ex.Message, t.Names + " " + t.LastName));
            }
        }
        //Get
        public Teacher Get(int id)
        {
            Teacher a = null;
            OracleCommand cmd = null;
            string query = @"SELECT P.Personid, P.names, P.lastName, 
                            COALESCE(P.secondLastName,''), P.addres, P.phone, P.birthDate,P.gender,
                            P.status,P.registrationDate,NVL(P.updateDate,'1900-01-01'),P.startDate,
                            NVL(P.finishDate,'1900-01-01'),P.email, P.latitude, P.longitude, P.TownId,
                            P.photo,T.teacherid FROM Person P 
                            INNER JOIN Teacher T ON T.PersonId=P.Personid WHERE P.Personid = :PersonId";
            OracleDataReader dr = null;
            try
            {
                DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":PersonId", id);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Teacher(int.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), DateTime.Parse(dr[6].ToString()), dr[7].ToString(), byte.Parse(dr[8].ToString()), DateTime.Parse(dr[9].ToString()), DateTime.Parse(dr[10].ToString()), DateTime.Parse(dr[11].ToString()), DateTime.Parse(dr[12].ToString()),dr[13].ToString(),Convert.ToDouble(dr[14].ToString()), Convert.ToDouble(dr[15].ToString()),byte.Parse(dr[16].ToString()),dr[17].ToString(),int.Parse(dr[18].ToString()));
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


        public DataTable Select()
        {

            string query = @"SELECT P.PersonId,
P.names Names,
P.lastName LastName, 
COALESCE(P.secondLastName,'') SecondLastName, 
P.addres Address,
P.phone Phone,
P.birthDate BirthDate,
P.gender Gender,
P.status Status,
P.registrationDate Registration,
NVL(P.updateDate,'1900-01-01') UpdateDate,
P.startDate StartDate,
NVL(P.finishDate,'1900-01-01') FinishDate,
P.email Mail,
P.latitude Latitude,
P.longitude Longitude,
(SELECT C.CityName||'-'||PR.provinceName||','||T.townName
FROM Town T INNER JOIN Province PR ON PR.ProvinceId=T.ProvinceId 
INNER JOIN City C ON C.CityId=PR.CityId WHERE P.TownId=T.TownId) Location 
FROM Person P 
INNER JOIN Teacher T ON T.PersonId = P.Personid 
INNER JOIN Users U ON U.Personid=P.Personid  WHERE P.status=0 AND U.role='P'";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        //SelectDis
        public DataTable SelectDis()
        {

            string query = @"SELECT P.PersonId,
P.names Names,
P.lastName LastName, 
COALESCE(P.secondLastName,'') SecondLastName, 
P.addres Address,
P.phone Phone,
P.birthDate BirthDate,
P.gender Gender,
P.status Status,
P.registrationDate Registration,
NVL(P.updateDate,'1900-01-01') UpdateDate,
P.startDate StartDate,
NVL(P.finishDate,'1900-01-01') FinishDate,
P.email Mail,
P.latitude Latitude,
P.longitude Longitude,
(SELECT C.CityName||'-'||PR.provinceName||','||T.townName
FROM Town T INNER JOIN Province PR ON PR.ProvinceId=T.ProvinceId 
INNER JOIN City C ON C.CityId=PR.CityId WHERE P.TownId=T.TownId) Location 
FROM Person P 
INNER JOIN Teacher T ON T.PersonId = P.Personid 
INNER JOIN Users U ON U.Personid=P.Personid  WHERE P.status=0 AND U.role='P'";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        public DataTable SelectSearch(string like)
        {

            string query = @"SELECT P.PersonId,
P.names Names,
P.lastName LastName, 
COALESCE(P.secondLastName,'') SecondLastName, 
P.addres Address,
P.phone Phone,
P.birthDate BirthDate,
P.gender Gender,
P.status Status,
P.registrationDate Registration,
NVL(P.updateDate,'1900-01-01') UpdateDate,
P.startDate StartDate,
NVL(P.finishDate,'1900-01-01') FinishDate,
P.email Mail,
P.latitude Latitude,
P.longitude Longitude,
(SELECT C.CityName||'-'||PR.provinceName||','||T.townName
FROM Town T INNER JOIN Province PR ON PR.ProvinceId=T.ProvinceId 
INNER JOIN City C ON C.CityId=PR.CityId WHERE P.TownId=T.TownId) Location 
FROM Person P 
INNER JOIN Teacher T ON T.PersonId = P.Personid 
INNER JOIN Users U ON U.Personid=P.Personid 
                            WHERE P.status=1 AND U.role='P' AND 
                            ((P.names LIKE :names) OR (P.lastName LIKE :last) OR (P.secondLastName LIKE :second))";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[3];

                parameters1[0] = new OracleParameter(":names", "%" + like + "%");
                parameters1[1] = new OracleParameter(":last", "%" + like + "%");
                parameters1[2] = new OracleParameter(":second", "%" + like + "%");

                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

    }
}
