using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace Implementation
{
    public class DBImplementation
    {

        #region oracle
        public double amount = 630;
        public static string connectionString = "DATA SOURCE = xe; PASSWORD = Univalle; USER ID = EducaOracle";
        public static string usermail = "educateam.suport@gmail.com";
        public static string passwordmail = "educa1597534682";
        public static string pathImages = @"d:\EducaImages/";
        #region Create Commands
        public static OracleCommand CreateBasicCommand()
        {
            OracleConnection connection = new OracleConnection(connectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            return cmd;
        }
        public static OracleCommand CreateBasicCommand(string query)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = connection;
            return cmd;
        }
        public static List<OracleCommand> CreateNBasicCommands(int n)
        {
            List<OracleCommand> res = new List<OracleCommand>();
            OracleConnection connection = new OracleConnection(connectionString);
            for (int i = 0; i < n; i++)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection;
                res.Add(cmd);
            }
            return res;
        }


        #endregion

        #region Execute Command
        public static int ExecuteBasicCommand(OracleCommand cmd)
        {
            int res = -1;
            try
            {
                cmd.Connection.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return res;
        }
        public static void ExecuteNBasicCommand(List<OracleCommand> cmds)
        {
            OracleTransaction transaction = null;
            OracleCommand cmd1 = cmds[0];
            try
            {
                
                cmd1.Connection.Open();
                transaction = cmd1.Connection.BeginTransaction();
                foreach (OracleCommand cmd in cmds)
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd1.Connection.Close();
            }
        }
        public static DataTable ExecuteDataTableCommand(OracleCommand cmd)
        {
            DataTable res = new DataTable();
            try
            {
                cmd.Connection.Open();
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Transact"+cmd.CommandText, DateTime.Now));
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return res;
        }

        public static OracleDataReader ExecuteDataReaderCommand(OracleCommand cmd)
        {
            OracleDataReader res = null;
            try
            {
                cmd.Connection.Open();
                res = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //acá no se cierra la conexión. se cierra una vez llamado al metodo
            return res;
        }
        #endregion

        #region Scalar
        public static string ExecuteScalarCommand(OracleCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static int GetIdentityFromTable(string table)
        {
            int res = -1;
            string query = "SELECT SEQ_" + table.ToUpper() + ".nextval FROM DUAL";
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;

        }

        public static void ResetIdentFromTable(string table)
        {
            int a = GetIncementFromTable(table);
            Alter1(table,a);
            GetIdentityFromTable(table);
            Alter2(table, a);

        }
        #region Reset
        public static void Alter1(string table, int a)
        {
            int res = -1;
            string query = "ALTER SEQUENCE SEQ_" + table.ToUpper() + " INCREMENT BY -"+a;
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void Alter2(string table, int a)
        {
            int res = -1;
            string query = "ALTER SEQUENCE SEQ_" + table.ToUpper() + " INCREMENT BY +" + a;
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        public static int GetNIdentityFromTable(string table, int n)
        {
            int res = -1;
            string query = "SELECT   " + table.ToUpper() + "_SEQ.currval+"+n+" +increment_by FROM user_sequences WHERE sequence_name = ' " + table.ToUpper() + "_SEQ'";
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public static int GetCurrentIdentityFromTable(string table)
        {
            int res = -1;
                string query = "SELECT  " + table.ToUpper() + "_SEQ.currval FROM user_sequences WHERE sequence_name = '" + table.ToUpper() + "_SEQ' ";
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public static int GetIncementFromTable(string table)
        {
            int res = -1;
            string query = "SELECT increment_by FROM user_sequences WHERE sequence_name = 'SEQ_" + table.ToUpper() + "'";
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public static int GetCountFromTable(string table)
        {
            int res = -1;
            string query = "SELECT COUNT(*) FROM " + table;
            try
            {
                OracleCommand cmd = CreateBasicCommand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        #endregion
        #endregion


        /*
        #region sql server
        public static string usermail = "educateam.suport@gmail.com";
        public static string passwordmail= "educa1597534682";
        public static string pathImages = @"d:\educaimages/";
        public static string connectionString = "data source = localhost; initial catalog = EducaDb; user id = sa; password = Univalle2019";
        
        #region Comands
        public static SqlCommand CreateBasicComand()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            return cmd;
        }

        public static SqlCommand CreateBasicComand(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = connection;
            return cmd;
        }
        #endregion

        #region Comands eject
        public static int GetIdentityFromTable(string table)
        {
            int res = -1;
            string query = "SELECT ISNULL(IDENT_CURRENT('" + table + "'),0) + IDENT_INCR('" + table + "')";
            try
            {
                SqlCommand cmd = CreateBasicComand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public static int GetIncementFromTable(string table)
        {
            int res = -1;
            string query = "SELECT IDENT_INCR('" + table + "')";
            try
            {
                SqlCommand cmd = CreateBasicComand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public static string ExecuteScalarCommand(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error: {1}.", DateTime.Now, ex.Message));
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static List<SqlCommand> CreateNBasicCommands(int n)
        {
            List<SqlCommand> res = new List<SqlCommand>();
            SqlConnection connection = new SqlConnection(connectionString);
            for (int i = 0; i < n; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                res.Add(cmd);
            }
            return res;
        }



        public static void ExecuteNBasicCommand(List<SqlCommand> cmds)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd1 = cmds[0];
            try
            {
                cmd1.Connection.Open();
                transaction = cmd1.Connection.BeginTransaction();
                foreach (SqlCommand cmd in cmds)
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd1.Connection.Close();
            }
        }


        public static int ExecuteBasicCommand(SqlCommand cmd)
        {
            int res = -1;
            try
            {
                cmd.Connection.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return res;
        }

        //metodo Select
        public static DataTable ExecuteDataTableCommand(SqlCommand cmd)
        {
            DataTable res = new DataTable();
            try
            {
                cmd.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return res;
        }

        //reader
        public static SqlDataReader ExecuteDataReaderCommand(SqlCommand cmd)
        {
            SqlDataReader res = null;
            try
            {
                cmd.Connection.Open();
                res = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //acá no se cierra la conexión. se cierra una vez llamado al metodo
            return res;
        }
        #endregion
        #endregion
        */ // sqlserver

        #region mail

        #endregion

    }
}
