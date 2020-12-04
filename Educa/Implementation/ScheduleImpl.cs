using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAO;
using System.Data;
using Oracle.DataAccess.Client;
namespace Implementation
{
    public class ScheduleImpl
    {
        #region addsch
        public DataTable Select(int idcourse)
        {

            string query = @"SELECT schedulesid,
CASE WHEN schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE  CO.idCourse= :course AND C.status>0) 
THEN 'O'
ELSE 'X'
END AS M,hourStart,hourFinish
FROM Schedules
WHERE day='Monday'";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                cmd.Parameters.Add(":course", idcourse);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        public DataTable Selectmar(int idcourse)
        {

            string query = @"SELECT schedulesid,
CASE WHEN schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE  CO.idCourse= :course AND C.status>0) 
THEN 'O'
ELSE 'X'
END AS TU,hourStart,hourFinish
FROM Schedules
WHERE day='Tuesday'";
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
        public DataTable Selectmier(int idcourse)
        {

            string query = @"SELECT schedulesid,
CASE WHEN schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE  CO.idCourse= :course AND C.status>0) 
THEN 'O'
ELSE 'X'
END AS W,hourStart,hourFinish
FROM Schedules
WHERE day='Wednesday' ";
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
        public DataTable Selectjue(int idcourse)
        {

            string query = @"SELECT schedulesid,
CASE WHEN schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE  CO.idCourse= :course AND C.status>0) 
THEN 'O'
ELSE 'X'
END AS TH,hourStart,hourFinish
FROM Schedules
WHERE day='Thursday'";
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
        public DataTable Selectvier(int idcourse)
        {

            string query = @"SELECT schedulesid,
CASE WHEN schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE  CO.idCourse= :course AND C.status>0) 
THEN 'O'
ELSE 'X'
END AS F,hourStart,hourFinish
FROM Schedules
WHERE day='Friday'";
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
        public DataTable Selectsab(int idcourse)
        {

            string query = @"SELECT schedulesid,
CASE WHEN schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE  CO.idCourse= :course AND C.status>0) 
THEN 'O'
ELSE 'X'
END AS S,hourStart,hourFinish
FROM Schedules
WHERE day='Saturday'
";
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
        #endregion

        public DataTable Selectlike(int idcourse,string like)
        {

            string query = @"SELECT schedulesid,hourStart,hourFinish,day FROM Schedules WHERE (schedulesid NOT IN 
(SELECT CS.schedulesid FROM ClassSchedules CS
INNER JOIN Class C ON C.idClass = CS.idClass
INNER JOIN Course CO ON CO.idCourse = C.idCourse
WHERE  CO.idCourse = :course)) AND day LIKE :like";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":course", idcourse);
                parameters1[0] = new OracleParameter(":like", "%" + like + "%");
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        #region teacherViews
        public DataTable SelectSpecificTeacherSchedule(int iduser, string day)
        {

            string query = @"SELECT 
COALESCE((SELECT M.matterName FROM ClassSchedules CS 
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Matter M ON M.idMatter=C.idMatter
INNER JOIN Teacher T ON T.teacherid=C.teacherid
INNER JOIN Person P ON P.Personid=T.PersonId
INNER JOIN Users U ON U.Personid=P.Personid
WHERE U.UserId= :user AND CS.schedulesid=S.schedulesid AND C.status=2),'') AS '" + day+@"'
FROM Schedules S
WHERE S.day= :day";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":user", iduser);
                parameters1[0] = new OracleParameter(":day", day);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ; }

        }

        #endregion

        #region studentView
        public DataTable SelectSpecificStudentSchedule(int iduser, string day)
        {

            string query = @"SELECT 
COALESCE((SELECT M.matterName FROM ClassSchedules CS 
INNER JOIN Class C ON C.idClass=CS.idClass
INNER JOIN Matter M ON M.idMatter=C.idMatter
INNER JOIN Course CO ON CO.idCourse=C.idCourse
INNER JOIN Student ST ON ST.idCourse=CO.idCourse
INNER JOIN Person P ON P.Personid=ST.PersonId
INNER JOIN Users U ON U.Personid=P.Personid
WHERE U.UserId= :user AND CS.schedulesid=S.schedulesid AND C.status>0),'') AS '" + day+@"'
FROM Schedules S
WHERE S.day= :day";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":user", iduser);
                parameters1[0] = new OracleParameter(":day", day);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }
        #endregion
    }
}
