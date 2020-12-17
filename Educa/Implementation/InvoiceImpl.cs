using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Oracle.DataAccess.Client;
using ControlCodeDosage;

namespace Implementation
{
    public class InvoiceImpl
    {
        public DataTable Selectlike( string like)
        {

            string query = @"SELECT S.STUDENTID, P.NAMES||' '||P.LASTNAME||' '||P.SECONDLASTNAME NAMES,
    (SELECT COUNT(IDPAYMENT) FROM PAYMENT PA WHERE PA.STUDENTID=S.STUDENTID AND PA.STATUS>0) ""PENDING PAYMENTS""
    FROM STUDENT S
    INNER JOIN PERSON P ON P.PERSONID=S.PERSONID
    INNER JOIN USERACCOUNT U ON U.PERSONID=P.PERSONID
    
    WHERE U.STATUS>0 AND (P.NAMES LIKE :likes OR P.LASTNAME LIKE :likes OR P.LASTNAME LIKE :likes OR P.NAMES||' '||P.LASTNAME LIKE :likes) ";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":likes", "%" + like + "%");
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        public Invoice Get(int id)
        {
            Invoice a = new Invoice();
            OracleCommand cmd = null;
            string query = @"SELECT S.STUDENTID, P.NAMES||' '||P.LASTNAME||' '||P.SECONDLASTNAME NAMES,
    (SELECT COUNT(IDPAYMENT) FROM PAYMENT PA WHERE PA.STUDENTID=S.STUDENTID AND PA.STATUS>0) ""PENDING PAYMENTS""
    FROM STUDENT S
    INNER JOIN PERSON P ON P.PERSONID = S.PERSONID
    INNER JOIN USERACCOUNT U ON U.PERSONID = P.PERSONID


    WHERE U.STATUS > 0 AND S.STUDENTID =:STUDENTID";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":STUDENTID", id);
                cmd.Parameters.AddRange(parameters1);

                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a.IdInvoide = int.Parse(dr[0].ToString());
                    a.BussinesName = dr[1].ToString();
                    a.IdPayer= int.Parse(dr[2].ToString());
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


        public void InsertTransaction(List<Payment> u,double total,Payer p)
        {
            string queryInvoice = @"INSERT INTO INVOICE(INVOICEDATE,TOTAL,NROINVOICE,CONTROLCODE,IDPAYER)
VALUES(CURRENT_TIMESTAMP,:TOTAL,:NROINVOICE,:CONTROLCODE,:IDPAYER)";


            string queryDetail= @"INSERT INTO DETAIL(DESCRIPTIONS,AMOUNT,IDPAYMENT,IDINVOICE)
VALUES(:DESCRIPTIONS,:AMOUNT,:IDPAYMENT,:IDINVOICE)";



            try
            {

                int details = 0;
                foreach (var d in u)
                {
                    details++;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Add Class.", DateTime.Now));
                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(1 );
                List<OracleParameter[]> parameters = new List<OracleParameter[]>();
                DosageImpl dosageImpl = new DosageImpl();
                Dosage dosage = dosageImpl.Get();
                int numberinvoice = dosage.FinalNumber + 1;
                string controlCode = ControlCode.generateControlCode(dosage.NroAuthorization.ToString(), numberinvoice.ToString(), p.Nit,
                                                                DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString(),
                                                                total.ToString(), dosage.DosageKey);
                int id = DBImplementation.GetIdentityFromTable("INVOICE");
                DBImplementation.ResetIdentFromTable("INVOICE");
                cmds[0].CommandText = queryInvoice;
                cmds[0].Parameters.Add(new OracleParameter(":TOTAL", total));
                cmds[0].Parameters.Add(new OracleParameter(":NROINVOICE", numberinvoice));
                cmds[0].Parameters.Add(new OracleParameter(":CONTROLCODE", controlCode));
                cmds[0].Parameters.Add(new OracleParameter(":IDPAYER", p.IdPayer));
                cmds[0].Parameters.AddRange(parameters[0]);



                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Invoice Succesfully.", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Add Class({1}).", DateTime.Now, ex.Message));
            }
        }

    }
}
