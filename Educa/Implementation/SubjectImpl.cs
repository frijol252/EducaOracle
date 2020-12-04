using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Implementation;
using System.Data;
using Oracle.DataAccess.Client;

namespace Implementation
{
    public class SubjectImpl
    {
        //SELECT
        public DataTable Selectasd(int idcourse)
        {

            string query = @"SELECT idMatter,matterName FROM Matter WHERE (idMatter NOT IN(SELECT idMatter FROM Class WHERE idCourse= 21 AND status>0))";

            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":course", idcourse);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        /*
        public DataTable Select(string id)
        {

            string query = "SELECT CONCAT() FROM";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":CityId", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }*/
        //GET

        #region teacher
        public DataTable SelectSpecificTeacherSubject(int iduser)
        {

            string query = @"SELECT C.idClass, M.matterName Subject, CO.numberCourse||CO.letterCourse Course
 FROM Class C
INNER JOIN Matter M ON C.idMatter=M.idMatter
INNER JOIN Course CO ON CO.idCourse=C.idCourse
INNER JOIN Teacher T ON T.teacherid=C.teacherid
INNER JOIN Person P ON P.Personid=T.PersonId
INNER JOIN Users U ON U.Personid=P.Personid
WHERE U.UserId= :user AND C.status=2";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":user", iduser);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }
        #endregion
    }
}
