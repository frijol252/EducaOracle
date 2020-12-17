using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Oracle.DataAccess.Client;
namespace Implementation
{
    public class UserImpl
    {
        public DataTable Login(string userName, string password)
        {
            DataTable res = new DataTable();
            string query = "SELECT U.USERID, U.USERNAME, U.PASSWORD, U.ROLE, P.NAMES||' '||P.LASTNAME, U.REVISIONPASS, P.EMAIL,P.PHOTO,U.STATUS FROM USERACCOUNT U INNER JOIN PERSON P ON P.PERSONID=U.PERSONID WHERE  U.USERNAME=:userName AND U.PASSWORD=standard_hash(:password, 'MD5')";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":userName", userName);
                parameters1[1] = new OracleParameter(":password", password); 
                cmd.Parameters.AddRange(parameters1);

                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Insert
        public int Insert(User t)
        {
            string query = @"INSERT INTO Users (userName, password, role,Personid)
                            VALUES( :userName,standard_hash(:password, 'MD5'), :role, :PersonId)";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[4];

                parameters1[0] = new OracleParameter(":userName", t.UserName);
                parameters1[1] = new OracleParameter(":password", t.UserName);
                parameters1[2] = new OracleParameter(":role", t.Role);
                parameters1[3] = new OracleParameter(":PersonIdsword", t.PersonId);
                cmd.Parameters.AddRange(parameters1);


                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //UPDATE password
        public int Updatepass(User t)
        {
            string query = @"UPDATE USERACCOUNT SET PASSWORD=standard_hash(:password, 'MD5'), REVISIONPASS=1, UPDATEDATE=CURRENT_TIMESTAMP WHERE USERID=:Userid";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                
                parameters1[0] = new OracleParameter(":password", t.Password);
                parameters1[1] = new OracleParameter(":Userid", t.UserID);
                cmd.Parameters.AddRange(parameters1);


                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}
