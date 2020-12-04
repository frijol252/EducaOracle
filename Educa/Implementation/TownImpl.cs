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
    public class TownImpl
    {
        public DataTable Select(string id, string id2)
        {

            string query = "SELECT DISTINCT T.TownId id, T.townName name FROM Town T INNER JOIN Province P ON T.ProvinceId=(SELECT ProvinceId FROM Province WHERE provinceName= :ProvinceName) INNER JOIN City C ON (SELECT CityId FROM Province WHERE provinceName= :ProvinceName)=(SELECT CityId FROM City WHERE CityName= :CityName)";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[2];

                parameters1[0] = new OracleParameter(":CityName", id);
                parameters1[1] = new OracleParameter(":ProvinceName", id2);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public Town GetID(string id, string id2,string id3)
        {
            Town a = null;
            OracleCommand cmd = null;
            string query = @"SELECT DISTINCT T.TownId FROM Town T INNER JOIN Province P ON T.ProvinceId=(SELECT ProvinceId FROM Province WHERE provinceName = :ProvinceName) INNER JOIN City C ON (SELECT CityId FROM Province WHERE provinceName= :ProvinceName)=(SELECT CityId FROM City WHERE CityName= :CityName) where t.townName= :townName";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[3];

                parameters1[0] = new OracleParameter(":CityName", id);
                parameters1[1] = new OracleParameter(":ProvinceName", id2);
                parameters1[2] = new OracleParameter(":townName", id3);
                cmd.Parameters.AddRange(parameters1);
                
                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Town(byte.Parse(dr[0].ToString()));
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

            string query = "SELECT DISTINCT [TownId] as 'id', TownName as 'name',ProvinceId as 'province' FROM Town  WHERE ProvinceId=(SELECT ProvinceId FROM Town WHERE TownId= :Town)";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":Town", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }



        public Town GetPlace(byte id)
        {
            Town a = null;
            OracleCommand cmd = null;
            string query = @"SELECT TownId, townName,ProvinceId FROM Town WHERE TownId= :townName";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":townName", id);
                cmd.Parameters.AddRange(parameters1);

                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a = new Town(byte.Parse(dr[0].ToString()),dr[1].ToString(),byte.Parse(dr[2].ToString()),byte.Parse(dr[3].ToString()));
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
