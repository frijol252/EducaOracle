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
    public class CityImpl
    {
        public DataTable Select()
        {

            string query = "SELECT CITYID AS ID, CITYNAME AS NAME FROM CITY WHERE STATE=1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public City GetPlace(byte id)
        {
            City a = null;
            OracleCommand cmd = null;
            OracleParameter[] parameters1 = new OracleParameter[1];
            string query = @"SELECT CityId, CityName,state FROM City WHERE CityId=:CityId";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                parameters1[0] = new OracleParameter(":CityId", id);
                cmd.Parameters.AddRange(parameters1);

                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new City(byte.Parse(dr[0].ToString()), dr[1].ToString(), byte.Parse(dr[2].ToString()));
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
        public DataTable SelectList()
        {

            string query = "SELECT DISTINCT CityId id, CityName name FROM City  ";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
