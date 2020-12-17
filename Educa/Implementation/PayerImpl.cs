using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Oracle.DataAccess.Client;

namespace Implementation
{
    public class PayerImpl
    {
        public Payer Get(string id)
        {
            Payer a = new Payer();
            OracleCommand cmd = null;
            string query = @"SELECT IDPAYER ,NIT,BUSINESSNAME
    FROM PAYER WHERE NIT=:NIT";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":NIT", id);
                cmd.Parameters.AddRange(parameters1);

                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a.IdPayer = int.Parse(dr[0].ToString());
                    a.Nit = dr[1].ToString();
                    a.BusinessName = dr[2].ToString();
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
        public void InsertTransaction(string Nit,string Buss)
        {

            string queryInsert = @"INSERT INTO PAYER(NIT,BUSINESSNAME)
                                    VALUES(:NIT,:BUSINESSNAME)";



            try
            {

                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Insert New Payer.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(1);

                cmds[0].CommandText = queryInsert;
                cmds[0].Parameters.Add(new OracleParameter(":NIT", Nit));
                cmds[0].Parameters.Add(new OracleParameter(":BUSINESSNAME", Buss));




                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Insert Payer Succesfuly.", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Add Payer({1}).", DateTime.Now, ex.Message));
            }
        }
    }
}
