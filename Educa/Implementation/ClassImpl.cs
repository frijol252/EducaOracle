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
    public class ClassImpl
    {
        #region studensSchedule
        public DataTable Select(int id)
        {

            string query = @"  SELECT CL.idClass,M.matterName SubjectName,C.numberCourse||C.letterCourse Course,(SELECT names||' '||lastName  FROM Person P
  INNER JOIN Teacher T ON T.PersonId=P.Personid WHERE T.teacherid=CL.teacherid) Teacher FROM Class CL 
  INNER JOIN Matter M ON M.idMatter=CL.idMatter
  INNER JOIN Course C ON C.idCourse=CL.idCourse
  WHERE CL.idCourse= :Course AND CL.status>0";
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

            string query = @"SELECT CL.idClass,M.matterName AS 'Subject Name',CONCAT(C.numberCourse,C.letterCourse) AS 'Course',(SELECT CONCAT(names,' ',lastName)  FROM Person P
INNER JOIN Teacher T ON T.PersonId = P.Personid WHERE T.teacherid = CL.teacherid) AS 'Teacher' FROM Class CL
INNER JOIN Matter M ON M.idMatter = CL.idMatter
INNER JOIN Course C ON C.idCourse = CL.idCourse
WHERE CL.idCourse = @Course AND ((SELECT CONCAT(names,' ',lastName)  FROM Person P
INNER JOIN Teacher T ON T.PersonId = P.Personid WHERE T.teacherid = CL.teacherid) LIKE @like OR M.matterName LIKE @like) AND CL.status>0";
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
        #endregion



        #region teacher
        public DataTable SelectTeacherActive(int id)
        {

            string query = @"SELECT CL.idClass,M.matterName AS 'Subject Name',CONCAT(C.numberCourse,C.letterCourse) AS 'Course',
STUFF(
    (SELECT DISTINCT ', ' + S.day
    FROM ClassSchedules CSS
    INNER JOIN Schedules S ON S.schedulesid= CSS.schedulesid
    WHERE CSS.idClass = CL.idClass
    FOR XML PATH ('')),
1,2, '') AS 'Days'
FROM Class CL 
  INNER JOIN Matter M ON M.idMatter=CL.idMatter
  INNER JOIN Teacher T ON T.teacherid=CL.teacherid
  INNER JOIN Course C ON C.idCourse=CL.idCourse
  WHERE T.teacherid= :Teacher and CL.status=2";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Teacher", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        public DataTable SelectTeacherAdd(int id)
        {

            string query = @"SELECT DISTINCT C.idClass,M.matterName AS 'Subject Name',CONCAT(CO.numberCourse,CO.letterCourse) AS 'Course',
STUFF(
    (SELECT DISTINCT ', ' + S.day
    FROM ClassSchedules CSS
    INNER JOIN Schedules S ON S.schedulesid= CSS.schedulesid
	
    WHERE CSS.idClass = C.idClass
    FOR XML PATH ('')),
1,2, '') AS 'Days'
 FROM Class C INNER JOIN ClassSchedules CS ON CS.idClass=C.idClass
 INNER JOIN Matter M ON M.idMatter=C.idMatter
  INNER JOIN Course CO ON CO.idCourse=C.idCourse
WHERE C.status=1 AND ( C.idClass  NOT IN
(SELECT DISTINCT CS.idClass FROM ClassSchedules CS INNER JOIN Class C ON C.idClass=CS.idClass
WHERE CS.schedulesid IN(SELECT CS.schedulesid
  FROM Teacher T
  INNER JOIN Class CL ON CL.teacherid=T.teacherid
  INNER JOIN ClassSchedules CS ON CS.idClass=CL.idClass
  
  WHERE T.teacherid= :Teacher)));";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Teacher", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        public DataTable SelectTeacherAdd(string like)
        {

            string query = @"SELECT C.idClass,M.matterName AS 'Subject Name',CONCAT(CO.numberCourse,CO.letterCourse) AS 'Course',
STUFF(
    (SELECT DISTINCT ', ' + S.day
    FROM ClassSchedules CSS
    INNER JOIN Schedules S ON S.schedulesid= CSS.schedulesid
    WHERE CSS.idClass = C.idClass
    FOR XML PATH ('')),
1,2, '') AS 'Days'
FROM Class C INNER JOIN Matter M ON C.idMatter=M.idMatter
INNER JOIN Course CO ON CO.idCourse=C.idCourse WHERE (M.matterName LIKE :like
OR CONCAT(CO.numberCourse,CO.letterCourse) LIKE :like 
OR CONCAT(M.matterName,CO.numberCourse,CO.letterCourse) LIKE :like
OR CONCAT(CO.numberCourse,CO.letterCourse,' ',M.matterName) LIKE :like ) AND C.status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter("@like", "%" + like + "%");
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }



        //INSERT TEACHER SUBJECTS
        public void InsertTeacherSubject(Class c)
        {
            string queryClass = @"UPDATE Class SET teacherid= :teacher, status=2 WHERE idClass= :class";

            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Add Class to Teacher.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(1);

                OracleParameter[] parameters1 = new OracleParameter[2];
                cmds[0].CommandText = queryClass;
                parameters1[0] = new OracleParameter(":teacher", c.TeacherId);
                parameters1[0] = new OracleParameter(":class", c.IdClass);
                cmds[0].Parameters.AddRange(parameters1);

                cmds[0].CommandText = queryClass;


                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Add Class to Teacher" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Add Class to Teacher({1}).", DateTime.Now, ex.Message));
            }
        }
        public void DelTeacherSubject(Class c)
        {
            string queryClass = @"UPDATE Class SET teacherid=6014, status=1 WHERE idClass= :class";

            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Add Class to Teacher.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(1);

                OracleParameter[] parameters1 = new OracleParameter[1];
                cmds[0].CommandText = queryClass;
                parameters1[0] = new OracleParameter(":class", c.IdClass);
                cmds[0].Parameters.AddRange(parameters1);


                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Add Class to Teacher" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Add Class to Teacher({1}).", DateTime.Now, ex.Message));
            }
        }
        #endregion


        #region addClass
        //INSERT
        public void InsertTransaction(int idsub,List<int> u,int count,int course)
        {
            string queryClass = @"INSERT INTO Class(idMatter,idCourse)
                                    VALUES( :subject , :course)";


            string queryClassSche= @"INSERT INTO ClassSchedules(idClass,schedulesid)
                                    VALUES( :idclass, :idsche)";

            string queryAve = @"INSERT INTO AverageGradeTotal(firstTrimester)
  VALUES(0) ";

            string queryGrades = @"INSERT INTO Grade(studentid,idClass,idAverage)
  VALUES( :student, :class, :average) ";

            string queryFirst = @"INSERT INTO FirstTrimester(gradeId)
  VALUES( :id)";

            string querySecond= @"INSERT INTO SecondTrimester(gradeId)
  VALUES( :id)";

            string queryThird= @"INSERT INTO ThirdTrimester(gradeId)
  VALUES( :id)";

            

            try
            {
                int i = 1;
                int idgradetotal= DBImplementation.GetIdentityFromTable("Grade");
                int idgradeincrement = DBImplementation.GetIncementFromTable("Grade");
                int idgrade = idgradetotal - idgradeincrement;
                int idaveragetotal = DBImplementation.GetIdentityFromTable("AverageGradeTotal");
                int idaverageincrement = DBImplementation.GetIncementFromTable("AverageGradeTotal");
                int idaverage = idaveragetotal - idaverageincrement;
                int students = 0;
                DataTable dt = new DataTable();
                dt = SelectStudents(course);
                foreach (DataRow d in dt.Rows)
                {
                    students++;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Add Class.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(count+(students*7));
                List<OracleParameter[]> parameters = new List<OracleParameter[]>();

                int id = DBImplementation.GetIdentityFromTable("Class");
                cmds[0].CommandText = queryClass;
                parameters.Add(new OracleParameter[2]);
                parameters[0][0] = new OracleParameter(":subject", idsub);
                parameters[0][1] = new OracleParameter(":course", course);
                cmds[0].Parameters.AddRange(parameters[0]);

                


                foreach (var lis in u)
                {
                    cmds[i].CommandText = queryClassSche;

                    parameters.Add(new OracleParameter[2]);
                    parameters[i][0] = new OracleParameter(":idclass", id);
                    parameters[i][1] = new OracleParameter(":idsche", lis);
                    cmds[i].Parameters.AddRange(parameters[i]);
                    i++;
                }
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
                                parameters[i][0] = new OracleParameter(":student", d[0].ToString());
                                parameters[i][1] = new OracleParameter(":class", id);
                                parameters[i][2] = new OracleParameter(":average", idaverage);
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
                                parameters[i][0] = new OracleParameter(":student", d[0].ToString());
                                parameters[i][1] = new OracleParameter(":class", id);
                                parameters[i][2] = new OracleParameter(":average", idaverage);
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
                                parameters[i][0] = new OracleParameter(":student", d[0].ToString());
                                parameters[i][1] = new OracleParameter(":class", id);
                                parameters[i][2] = new OracleParameter(":average", idaverage);
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                cmds[i].CommandText = queryThird; 
                                cmds[i].Parameters.AddRange(parameters[i]);
                                i++;
                                break;
                        }
                    }
                }
                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Add Class by" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Add Class({1}).", DateTime.Now, ex.Message));
            }
        }

        public void DeleteTransaction(int idsub)
        {
            string queryClass = @"UPDATE Class SET status=0 WHERE idClass= :class";
            



            try
            {
                int i = 1;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Del Class.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(1);

                OracleParameter[] parameters1 = new OracleParameter[1];
                cmds[0].CommandText = queryClass;
                parameters1[0] = new OracleParameter(":class", idsub);
                cmds[0].Parameters.AddRange(parameters1);

                cmds[0].CommandText = queryClass;

                


                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Dele Class by" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }
        #endregion

        //GET
        public Class Get(int id)
        {
            Class a = null;
            OracleCommand cmd = null;
            string query = @"SELECT C.idClass FROM Class C WHERE C.idClass = :classId";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":classId", id);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Class(int.Parse(dr[0].ToString()));
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

        //SELECT STUDENTLIST
        public DataTable SelectStudents(int id)
        {

            string query = @"SELECT studentId FROM Student WHERE idCourse= :Course";
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

    }
}
