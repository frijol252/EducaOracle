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
    public class ProvinceImpl
    {
        public DataTable Select(string id)
        {

            string query = "SELECT PROVINCEID, PROVINCENAME FROM Province WHERE STATE=1 and CITYID=:CityId";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":CityId", int.Parse(id));
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public Province GetPlace(byte id)
        {
            Province a = null;
            OracleCommand cmd = null;
            string query = @"SELECT ProvinceId, provinceName,state,CityId FROM Province WHERE ProvinceId= :proviceName";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":proviceName", id);
                cmd.Parameters.AddRange(parameters1);
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Province(byte.Parse(dr[0].ToString()), dr[1].ToString(), byte.Parse(dr[2].ToString()), byte.Parse(dr[3].ToString()));
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
        public DataTable SelectList(int id)
        {

            string query = "SELECT DISTINCT ProvinceId id, provinceName name,CityId city FROM Province  WHERE CityId=(SELECT CityId FROM Province WHERE ProvinceId= :province)";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":province", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
