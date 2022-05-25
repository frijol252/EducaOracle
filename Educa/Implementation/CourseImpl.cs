using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using System.Data;
using Model;

using Oracle.DataAccess.Client;
namespace Implementation
{
    public class CourseImpl
    {
        //SELECT
        public DataTable Select()
        {

            string query = @"SELECT idCourse,numberCourse||letterCourse Course,section Section,
status Status, registrationDate Registration,NVL(updateDate, '01-01-1993') UpdateDate FROM Course";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        public DataTable SelectCrystal()
        {

            string query = @"SELECT idCourse,numberCourse,letterCourse,section Section,
status Status, registrationDate Registration,NVL(updateDate, '01-01-1993') UpdateDate FROM Course";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        public DataTable Selectlike(string like)
        {

            string query = "SELECT idCourse,numberCourse||letterCourse Course,section Section,status Status,registrationDate Registration,updateDate UpdateDate FROM Course WHERE numberCourse||letterCourse LIKE :likes OR section LIKE :likes OR numberCourse||letterCourse||section LIKE :likes OR numberCourse||letterCourse||' '||section LIKE :likes";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":likes", "%" + like + "%");
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        //GET
        public Course Get(int id)
        {
            Course a = null;
            OracleCommand cmd = null;
            string query = @"SELECT idCourse,numberCourse,letterCourse,section FROM Course WHERE idCourse= :Course";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Course", id);
                cmd.Parameters.AddRange(parameters1);

                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Course(int.Parse(dr[0].ToString()), int.Parse(dr[1].ToString()), dr[2].ToString(), dr[3].ToString());
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
