using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;
using Oracle.DataAccess.Client;
namespace Implementation
{
    public class HorarioImpl
    {
        
        //Insert
        public int Insert(Horario t)
        {
            string query = @"INSERT INTO timetable (hourStart, hourFinish, day)
                            VALUES( :horaIn, :horaFin, :Day)";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":horaIn", t.HoraInicio);
                parameters1[1] = new OracleParameter(":horaFin", t.HoraFin);
                parameters1[2] = new OracleParameter(":Day", t.Day);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //Delete
        public int Delete(Horario t)
        {
            string query = @"UPDATE timetable SET status=0 Where timeTableid= :idHorario";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":idHorario", t.idHorario);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteBasicCommand(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Select
        public DataTable Select()
        {

            string query = "SELECT * FROM timetable WHERE status=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);

                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        //Update
        public int Update(Horario t)
        {
            string query = @"UPDATE timetable SET hourStart= :horaIn, hourFinish= :horaFin, updateDate=CURRENT_TIMESTAMP, day= :Day WHERE timeTableid= :idHorario";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[4];

                parameters1[0] = new OracleParameter(":horaIn", t.HoraInicio);
                parameters1[1] = new OracleParameter(":horaFin", t.HoraFin);
                parameters1[2] = new OracleParameter(":Day", t.Day);
                parameters1[3] = new OracleParameter(":idHorario", t.idHorario);
                cmd.Parameters.AddRange(parameters1);

                return DBImplementation.ExecuteBasicCommand(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public Horario Get(byte id)
        {
            Horario a = null;
            OracleCommand cmd = null;
            string query = @"SELECT timeTableId, hourStart, hourFinish, ISNULL(updateDate,0), status, registrationDate, day
                            FROM timetable
                            WHERE timeTableId = :timeTableId";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":timeTableId", id);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Horario(byte.Parse(dr[0].ToString()), DateTime.Parse(dr[1].ToString()), DateTime.Parse(dr[2].ToString()), DateTime.Parse(dr[3].ToString()) , byte.Parse(dr[4].ToString()), DateTime.Parse(dr[5].ToString()), dr[6].ToString());
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
