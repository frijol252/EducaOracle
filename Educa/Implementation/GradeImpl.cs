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
    public class GradeImpl
    {
        public DataTable SelectStudentsFirst(int idclass)
        {

            string query = @"SELECT G.gradeId, P.names||' '||P.lastName StudentName,
G.grade1 Grade1,G.grade2 Grade2,G.grade3 Grade3,G.grade4 Grade4
,G.testGrade TestGrade,G.average Average,G.idAverage
 FROM Class C
INNER JOIN Grade G ON C.idClass=G.idClass
INNER JOIN FirstTrimester F ON F.gradeId=G.gradeId
INNER JOIN Student S ON S.studentId=G.studentid
INNER JOIN Person P ON P.Personid=S.PersonId
INNER JOIN USERACCOUNT U ON U.Personid=P.Personid
WHERE C.idClass= :class AND U.status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":class", idclass);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }
        public DataTable SelectStudentsSecond(int idclass)
        {

            string query = @"SELECT G.gradeId, P.names||' '||P.lastName StudentName,
G.grade1 Grade1,G.grade2 Grade2,G.grade3 Grade3,G.grade4 Grade4
,G.testGrade TestGrade,G.average Average,G.idAverage
 FROM Class C
INNER JOIN Grade G ON C.idClass=G.idClass
INNER JOIN SecondTrimester F ON F.gradeId=G.gradeId
INNER JOIN Student S ON S.studentId=G.studentid
INNER JOIN Person P ON P.Personid=S.PersonId
INNER JOIN USERACCOUNT U ON U.Personid=P.Personid
WHERE C.idClass= :class AND U.status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":class", idclass);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }
        public DataTable SelectStudentsThird(int idclass)
        {

            string query = @"SELECT G.gradeId, P.names||' '||P.lastName StudentName,
G.grade1 Grade1,G.grade2 Grade2,G.grade3 Grade3,G.grade4 Grade4
,G.testGrade TestGrade,G.average Average,G.idAverage
 FROM Class C
INNER JOIN Grade G ON C.idClass=G.idClass
INNER JOIN ThirdTrimester F ON F.gradeId=G.gradeId
INNER JOIN Student S ON S.studentId=G.studentid
INNER JOIN Person P ON P.Personid=S.PersonId
INNER JOIN USERACCOUNT U ON U.Personid=P.Personid
WHERE C.idClass= :class AND U.status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":class", idclass);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }


        #region UpdateGrades
        public void UpdateTransactionFirst(Grade g,double tot, double sum)
        {
            string queryGrade= @"UPDATE GRADE SET GRADE1 = :grade1,GRADE2= :grade2,GRADE3= :grade3,GRADE4= :grade4,
                                   TESTGRADE= :testgrade,AVERAGE=:average,UPDATEDATE=CURRENT_TIMESTAMP WHERE GRADEID= :gradeid";
            string queryAverage= @"UPDATE AVERAGEGRADETOTAL SET FIRSTTRIMESTER= :tot,TOTALAVERAGE = :suma WHERE IDAVERAGE= :idAverage";




            try
            {
                int i = 1;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Update Grades.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);

                OracleParameter[] parameters1 = new OracleParameter[7];
                cmds[0].CommandText = queryGrade;
                parameters1[0] = new OracleParameter(":grade1", g.Grade1);
                parameters1[1] = new OracleParameter(":grade2", g.Grade2);
                parameters1[2] = new OracleParameter(":grade3", g.Grade3);
                parameters1[3] = new OracleParameter(":grade4", g.Grade4);
                parameters1[4] = new OracleParameter(":testgrade", g.Testgrade);
                parameters1[5] = new OracleParameter(":average", g.Average);
                parameters1[6] = new OracleParameter(":gradeid", g.IdGrado);
                cmds[0].Parameters.AddRange(parameters1);

                OracleParameter[] parameters2 = new OracleParameter[3];
                cmds[1].CommandText = queryAverage;
                parameters2[0] = new OracleParameter(":tot", tot);
                parameters2[1] = new OracleParameter(":suma", sum);
                parameters2[2] = new OracleParameter(":idAverage",g.IdAverage);
                cmds[1].Parameters.AddRange(parameters2);



                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Dele Class by" + Session.SessionCurrent.ToString() + " Object Send: {1}", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }
        public void UpdateTransactionSecond(Grade g, double tot, double sum)
        {
            string queryGrade = @"UPDATE GRADE SET GRADE1 = :grade1,GRADE2= :grade2,GRADE3= :grade3,GRADE4= :grade4,
                                   TESTGRADE= :testgrade,AVERAGE=:average,UPDATEDATE=CURRENT_TIMESTAMP WHERE GRADEID= :gradeid";
            string queryAverage = @"UPDATE AVERAGEGRADETOTAL SET SECONTTRIMESTER= :tot,TOTALAVERAGE = :suma WHERE IDAVERAGE= :idAverage";




            try
            {
                int i = 1;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Update Grades.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);

                OracleParameter[] parameters1 = new OracleParameter[7];
                cmds[0].CommandText = queryGrade;
                parameters1[0] = new OracleParameter(":grade1", g.Grade1);
                parameters1[1] = new OracleParameter(":grade2", g.Grade2);
                parameters1[2] = new OracleParameter(":grade3", g.Grade3);
                parameters1[3] = new OracleParameter(":grade4", g.Grade4);
                parameters1[4] = new OracleParameter(":testgrade", g.Testgrade);
                parameters1[5] = new OracleParameter(":average", g.Average);
                parameters1[6] = new OracleParameter(":gradeid", g.IdGrado);
                cmds[0].Parameters.AddRange(parameters1);

                OracleParameter[] parameters2 = new OracleParameter[3];
                cmds[1].CommandText = queryAverage;
                parameters2[0] = new OracleParameter(":tot", tot);
                parameters2[1] = new OracleParameter(":suma", sum);
                parameters2[2] = new OracleParameter(":idAverage", g.IdAverage);
                cmds[1].Parameters.AddRange(parameters2);




                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Send grades by" + Session.SessionCurrent.ToString() + DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not send Grades({1}).", DateTime.Now, ex.Message));
            }
        }
        public void UpdateTransactionThird(Grade g, double tot,double sum)
        {
            string queryGrade = @"UPDATE GRADE SET GRADE1 = :grade1,GRADE2= :grade2,GRADE3= :grade3,GRADE4= :grade4,
                                   TESTGRADE= :testgrade,AVERAGE=:average,UPDATEDATE=CURRENT_TIMESTAMP WHERE GRADEID= :gradeid";
            string queryAverage = @"UPDATE AVERAGEGRADETOTAL SET THIRDTRIMESTER= :tot,TOTALAVERAGE = :suma WHERE IDAVERAGE= :idAverage";




            try
            {
                int i = 1;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Update Grades.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);

                OracleParameter[] parameters1 = new OracleParameter[7];
                cmds[0].CommandText = queryGrade;
                parameters1[0] = new OracleParameter(":grade1", g.Grade1);
                parameters1[1] = new OracleParameter(":grade2", g.Grade2);
                parameters1[2] = new OracleParameter(":grade3", g.Grade3);
                parameters1[3] = new OracleParameter(":grade4", g.Grade4);
                parameters1[4] = new OracleParameter(":testgrade", g.Testgrade);
                parameters1[5] = new OracleParameter(":average", g.Average);
                parameters1[6] = new OracleParameter(":gradeid", g.IdGrado);
                cmds[0].Parameters.AddRange(parameters1);

                OracleParameter[] parameters2 = new OracleParameter[3];
                cmds[1].CommandText = queryAverage;
                parameters2[0] = new OracleParameter(":tot", tot);
                parameters2[1] = new OracleParameter(":suma", sum);
                parameters2[2] = new OracleParameter(":idAverage", g.IdAverage);
                cmds[1].Parameters.AddRange(parameters2);



                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Send grades by" + Session.SessionCurrent.ToString() + DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not send Grades({1}).", DateTime.Now, ex.Message));
            }
        }

        public DataTable SelectTotal(int idclass)
        {

            string query = @"select * from AverageGradeTotal WHERE idAverage= :tot";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":tot", idclass);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }
        #endregion
        #region ViewGrades
        public DataTable SelectStudentsGrades(int user)
        {

            string query = @"SELECT M.matterName ""Subject Name"",
(SELECT P.names||' '||P.lastName FROM Teacher T INNER JOIN Person P ON P.Personid=T.PersonId
 WHERE T.TEACHERID=C.TEACHERID) ""Teacher"",
        (SELECT G.average FROM Grade G
        INNER JOIN FirstTrimester F ON F.gradeId=G.gradeId
        WHERE G.idClass= C.idClass AND G.studentid= S.studentId) ""First Trimester"",
        (SELECT G.average FROM Grade G
        INNER JOIN SecondTrimester F ON F.gradeId=G.gradeId
        WHERE G.idClass= C.idClass AND G.studentid= S.studentId)""Second Trimester"",
        (SELECT G.average FROM Grade G
        INNER JOIN ThirdTrimester F ON F.gradeId=G.gradeId
        WHERE G.idClass= C.idClass AND G.studentid= S.studentId) ""Third Trimester"",
        (SELECT DISTINCT AV.TotalAverage FROM Grade G
        INNER JOIN AverageGradeTotal AV ON AV.idAverage= G.idAverage
        WHERE G.idClass= C.idClass AND G.studentid= S.studentId) ""Total Average""
        FROM Class C
        INNER JOIN Matter M ON M.idMatter=C.idMatter
        INNER JOIN Course CO ON CO.idCourse=C.idCourse
        INNER JOIN Student S ON S.idCourse=CO.idCourse
        INNER JOIN Person P ON P.Personid=S.PersonId
        INNER JOIN USERACCOUNT U ON U.Personid=P.Personid
        WHERE C.status>0 AND U.UserId=:userAccount";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":userAccount", user);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw; }

        }

        #endregion
    }
}
