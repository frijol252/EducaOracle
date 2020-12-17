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
    public class PaymentImpl
    {
        public DataTable Select(int id)
        {

            string query = @"SELECT PA.IDPAYMENT,PA.DETAIL ""DETAIL PAYMENT"",PA.AMOUNT,PA.PAIDOUT,PA.AMOUNT-PA.PAIDOUT BALANCE
    FROM STUDENT S
    INNER JOIN PERSON P ON P.PERSONID = S.PERSONID
    INNER JOIN USERACCOUNT U ON U.PERSONID = P.PERSONID
    INNER JOIN PAYMENT PA ON PA.STUDENTID = S.STUDENTID


    WHERE U.STATUS > 0 AND S.STUDENTID =:STUDENTID AND PA.STATUS = 1";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];
                parameters1[0] = new OracleParameter(":STUDENTID", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        
    }
}
