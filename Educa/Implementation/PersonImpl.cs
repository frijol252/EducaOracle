using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Oracle.DataAccess.Client;
namespace Implementation
{
    public class PersonImpl
    {
        public int Update(Person t)
        {
            string query = @"UPDATE Person SET names= :names, lastName= :lastName, secondLastName= :secondLastName, address= :address, phone= :phone, email= :email, photo= :photo, updateDate=CURRENT_TIMESTAMP WHERE Personid= :PersonId";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[8];

                parameters1[0] = new OracleParameter(":PersonId", t.PersonId);
                parameters1[0] = new OracleParameter(":names", t.Names);
                parameters1[0] = new OracleParameter(":lastName", t.LastName);
                parameters1[0] = new OracleParameter(":secondLastName", t.SecondLastName);
                parameters1[0] = new OracleParameter(":phone", t.Address);
                parameters1[0] = new OracleParameter(":email", t.Phone);
                parameters1[0] = new OracleParameter(":class", t.Email);
                if (t.Pathrevision == t.PathImage)
                { 
                    parameters1[0] = new OracleParameter(":photo", t.Photo);
                }
                else
                {
                    if (int.Parse(t.Photo)==0)
                    {
                        parameters1[0] = new OracleParameter(":photo", t.PersonId);


                        File.Copy(t.PathImage, DBImplementation.pathImages + t.PersonId + ".png");
                    }
                    else
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        parameters1[0] = new OracleParameter(":photo", t.PersonId);
                        File.Delete(DBImplementation.pathImages + t.PersonId + ".png");
                        File.Copy(t.PathImage, DBImplementation.pathImages + t.PersonId + ".png");
                    }
                }
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteBasicCommand(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Person Get(int id)
        {
            Person a = null;
            OracleCommand cmd = null;
            string query = @"SELECT P.Personid, P.names, P.lastName, COALESCE(P.secondLastName,''), P.addres, P.phone, P.birthDate,P.gender,P.status,P.registrationDate,NVL(P.updateDate,'01/01/1900'),P.startDate,NVL(P.finishDate,'01/01/1900'),P.email, P.latitude, P.longitude, P.TownId,P.photo FROM Person P INNER JOIN Users U ON U.PersonId=P.Personid WHERE U.UserId= :PersonId";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":PersonId", id);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Person(int.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), DateTime.Parse(dr[6].ToString()), dr[7].ToString(), byte.Parse(dr[8].ToString()), DateTime.Parse(dr[9].ToString()), DateTime.Parse(dr[10].ToString()), DateTime.Parse(dr[11].ToString()), DateTime.Parse(dr[12].ToString()), dr[13].ToString(), Convert.ToDouble(dr[14].ToString()), Convert.ToDouble(dr[15].ToString()), byte.Parse(dr[16].ToString()), dr[17].ToString());
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
        public Person GetPass(string user)
        {
            Person a = null;
            OracleCommand cmd = null;
            string query = @"SELECT P.email,U.UserId FROM Users U INNER JOIN Person P ON P.Personid=U.Personid WHERE U.userName= :User";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":User", user);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Person(dr[0].ToString(),int.Parse(dr[1].ToString()));
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
    }
}
