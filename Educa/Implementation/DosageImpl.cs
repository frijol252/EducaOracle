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
    public class DosageImpl
    {
        public Dosage Get()
        {
            Dosage a = new Dosage();
            OracleCommand cmd = null;
            string query = @"SELECT IDDOSAGE,NROAUTHORIZATION,DOSAGEKEY,COALESCE(FINALNUMBER,0) FROM DOSAGE WHERE STATUS>0";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a.IdDosage=int.Parse(dr[0].ToString());
                    a.NroAuthorization = long.Parse(dr[1].ToString());
                    a.DosageKey = dr[2].ToString();
                    a.FinalNumber = int.Parse(dr[2].ToString());
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
        public void InsertTransaction(Model.Dosage d)
        {
            string queryUpdate = @"UPDATE DOSAGE SET STATUS=0";


            string queryInsert= @"INSERT INTO DOSAGE(NROAUTHORIZATION,DEADLINE,DOSAGEKEY)
    VALUES(:NROAUTHORIZATION,ADD_MONTHS(CURRENT_TIMESTAMP,6),:DOSAGEKEY)";



            try
            {
                
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Insert New Dosage.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2);

                cmds[0].CommandText = queryUpdate;

                cmds[1].CommandText = queryInsert;
                cmds[1].Parameters.Add(new OracleParameter(":NROAUTHORIZATION", d.NroAuthorization));
                cmds[1].Parameters.Add(new OracleParameter(":DOSAGEKEY", d.DosageKey));




                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Insert Dosage Succesfuly.", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Add Dosage({1}).", DateTime.Now, ex.Message));
            }
        }
    }
}
