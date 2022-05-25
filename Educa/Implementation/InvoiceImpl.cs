using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Oracle.DataAccess.Client;

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
            string queryInvoice = @"INSERT INTO INVOICE(INVOICEDATE,TOTAL,NROINVOICE,CONTROLCODE,IDPAYER,IDDOSAGE,LITERAL)
VALUES(CURRENT_TIMESTAMP,:TOTAL,:NROINVOICE,:CONTROLCODE,:IDPAYER,:IDDOSAGE,:LITERAL)";


            string queryDetail= @"INSERT INTO DETAIL(DESCRIPTIONS,AMOUNT,IDPAYMENT,IDINVOICE)
VALUES(:DESCRIPTIONS,:AMOUNT,:IDPAYMENT,:IDINVOICE)";
            string queryPayment= @"UPDATE PAYMENT SET PAIDOUT=PAIDOUT+:PAIDOUT, STATUS=:STATUS WHERE IDPAYMENT=:IDPAYMENT";
            string queryDosage= @"UPDATE DOSAGE SET FINALNUMBER=COALESCE(FINALNUMBER,0)+1 WHERE IDDOSAGE=:IDDOSAGE";

            

            try
            {

                int details = 0;
                int i = 1;
                foreach (var d in u)
                {
                    details++;
                }
                Debug.Listeners.Clear();
                Debug.Listeners.Add(Logss.LogInternalActivities);
                Debug.WriteLine(string.Format("{0} Info: Start Insert Payment{1}", DateTime.Now, Session.SessionCurrent + " Administraor"));
                Debug.Flush();

                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2+(details*2));
                List<OracleParameter[]> parameters = new List<OracleParameter[]>();
                DosageImpl dosageImpl = new DosageImpl();
                Dosage dosage = dosageImpl.Get();
                int numberinvoice = dosage.FinalNumber + 1;
                string controlCode = ControlDosage.ControlCode.generateControlCode(dosage.NroAuthorization.ToString(), numberinvoice.ToString(), p.Nit,
                                                                DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString(),
                                                                total.ToString(), dosage.DosageKey);
                int id = DBImplementation.GetIdentityFromTable("INVOICE");
                DBImplementation.ResetIdentFromTable("INVOICE");
                cmds[0].CommandText = queryInvoice;
                cmds[0].Parameters.Add(new OracleParameter(":TOTAL", total));
                cmds[0].Parameters.Add(new OracleParameter(":NROINVOICE", numberinvoice));
                cmds[0].Parameters.Add(new OracleParameter(":CONTROLCODE", controlCode));
                cmds[0].Parameters.Add(new OracleParameter(":IDPAYER", p.IdPayer));
                cmds[0].Parameters.Add(new OracleParameter(":IDDOSAGE", dosage.IdDosage));
                cmds[0].Parameters.Add(new OracleParameter(":LITERAL", ControlDosage.Conversores.NumeroALetras(Convert.ToDecimal(total.ToString().Replace(",",".")))));
                foreach (var l in u)
                {
                    
                    cmds[i].CommandText = queryDetail;
                    cmds[i].Parameters.Add(new OracleParameter(":DESCRIPTIONS", "Pay Payment"));
                    cmds[i].Parameters.Add(new OracleParameter(":AMOUNT", l.Paidout));
                    cmds[i].Parameters.Add(new OracleParameter(":IDPAYMENT", l.IdPayment));
                    cmds[i].Parameters.Add(new OracleParameter(":IDINVOICE", id));
                    i++;
                    cmds[i].CommandText = queryPayment;
                    cmds[i].Parameters.Add(new OracleParameter(":PAIDOUT", l.Paidout));
                    cmds[i].Parameters.Add(new OracleParameter(":STATUS", l.Status));
                    cmds[i].Parameters.Add(new OracleParameter(":IDPAYMENT", l.IdPayment));
                    i++;
                }
                cmds[i].CommandText = queryDosage;
                cmds[i].Parameters.Add(new OracleParameter(":IDDOSAGE", dosage.IdDosage));

                DBImplementation.ExecuteNBasicCommand(cmds);
                Debug.WriteLine(string.Format("{0} Info: Payment add Succesfuly{1}", DateTime.Now, Session.SessionCurrent + " Administraor"));
                Debug.Flush();
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Payment Succesfully.", DateTime.Now));

            }
            catch (Exception ex)
            {
                Debug.Listeners.Clear();
                Debug.Listeners.Add(Logss.LogError);
                Debug.WriteLine(string.Format(string.Format("{0} | Error:  Could not Success Payment({1}).", DateTime.Now, ex.Message)));
                Debug.Flush();
            }
        }

        public DataTable SelectInvoice(int id)
        {

            string query = @"SELECT PE.LASTNAME||' '||PE.SECONDLASTNAME||' '||PE.NAMES ""FULLNAME"",
C.SECTION || ' ' || C.NUMBERCOURSE || C.LETTERCOURSE ""COURSE"",
PA.BUSINESSNAME ""BUSINESSNAME"",PA.NIT ""PAYER"",
CASE EXTRACT(month FROM I.INVOICEDATE)
WHEN 1 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF JANUARY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 2 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF FEBRUARY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 3 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF MARCH OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 4 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF APRIL OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 5 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF MAY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 6 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF JUNE OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 7 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF JULY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 8 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF AUGUST OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 9 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF SEPTEMBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 10 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF OCTOBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 11 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF NOVEMBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 12 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF DECEMBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
END ""DATE"",
U.USERNAME ""CODE"",TO_CHAR(I.NROINVOICE) ""NroINVOICE"",TO_CHAR(DO.NROAUTHORIZATION) ""NroAUTHORIZATION"",
'" + DBImplementation.nit + @" ' ""NIT"",I.CONTROLCODE ""CONTROLCODE"",DO.DEADLINE ""DEADLINE"",I.LITERAL ""LITERALAMOUNT"",
I.TOTAL ""AMOUNTTOTAL"",P.DETAIL ""DETAIL"",D.AMOUNT,
'" + DBImplementation.nit + @"|'||I.NROINVOICE||'|'||DO.NROAUTHORIZATION||'|'
||I.INVOICEDATE||'|'||I.TOTAL||'|'||I.TOTAL||'|'||
I.CONTROLCODE||'|'||PA.NIT||'|'||'0.00'||'|'||'0.00'||'|'||'0.00' ""QR""
FROM STUDENT S
INNER JOIN COURSE C ON C.IDCOURSE = S.IDCOURSE
INNER JOIN PERSON PE ON PE.PERSONID = S.PERSONID
INNER JOIN USERACCOUNT U ON PE.PERSONID = U.PERSONID
INNER JOIN PAYMENT P ON P.STUDENTID = S.STUDENTID
INNER JOIN DETAIL D ON D.IDPAYMENT = P.IDPAYMENT
INNER JOIN INVOICE I ON I.IDINVOICE = D.IDINVOICE
INNER JOIN PAYER PA ON PA.IDPAYER = I.IDPAYER
INNER JOIN DOSAGE DO ON DO.IDDOSAGE = I.IDDOSAGE 
WHERE I.IDINVOICE=:IDINVOICE";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":IDINVOICE", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        public DataTable SelectInvoices(string CONTROLCODE)
        {

            string query = @"SELECT PE.LASTNAME||' '||PE.SECONDLASTNAME||' '||PE.NAMES ""FULLNAME"",
C.SECTION || ' ' || C.NUMBERCOURSE || C.LETTERCOURSE ""COURSE"",
PA.BUSINESSNAME ""BUSINESSNAME"",PA.NIT ""PAYER"",
CASE EXTRACT(month FROM I.INVOICEDATE)
WHEN 1 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF JANUARY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 2 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF FEBRUARY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 3 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF MARCH OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 4 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF APRIL OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 5 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF MAY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 6 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF JUNE OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 7 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF JULY OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 8 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF AUGUST OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 9 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF SEPTEMBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 10 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF OCTOBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 11 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF NOVEMBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
WHEN 12 THEN EXTRACT(DAY FROM I.INVOICEDATE)|| ' OF DECEMBER OF ' || EXTRACT(YEAR FROM I.INVOICEDATE)
END ""DATE"",
U.USERNAME ""CODE"",TO_CHAR(I.NROINVOICE) ""NroINVOICE"",TO_CHAR(DO.NROAUTHORIZATION) ""NroAUTHORIZATION"",
'" + DBImplementation.nit + @" ' ""NIT"",I.CONTROLCODE ""CONTROLCODE"",DO.DEADLINE ""DEADLINE"",I.LITERAL ""LITERALAMOUNT"",
I.TOTAL ""AMOUNTTOTAL"",P.DETAIL ""DETAIL"",D.AMOUNT,
'" + DBImplementation.nit + @"|'||I.NROINVOICE||'|'||DO.NROAUTHORIZATION||'|'
||I.INVOICEDATE||'|'||I.TOTAL||'|'||I.TOTAL||'|'||
I.CONTROLCODE||'|'||PA.NIT||'|'||'0.00'||'|'||'0.00'||'|'||'0.00' ""QR""
FROM STUDENT S
INNER JOIN COURSE C ON C.IDCOURSE = S.IDCOURSE
INNER JOIN PERSON PE ON PE.PERSONID = S.PERSONID
INNER JOIN USERACCOUNT U ON PE.PERSONID = U.PERSONID
INNER JOIN PAYMENT P ON P.STUDENTID = S.STUDENTID
INNER JOIN DETAIL D ON D.IDPAYMENT = P.IDPAYMENT
INNER JOIN INVOICE I ON I.IDINVOICE = D.IDINVOICE
INNER JOIN PAYER PA ON PA.IDPAYER = I.IDPAYER
INNER JOIN DOSAGE DO ON DO.IDDOSAGE = I.IDDOSAGE 
WHERE I.CONTROLCODE=:CONTROLCODE";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":CONTROLCODE", CONTROLCODE);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        public DataTable SelectPayments(int id)
        {

            string query = @"SELECT IDPAYMENT,AMOUNT FROM DETAIL WHERE IDINVOICE=:IDINVOICE";
            try
            {
                OracleCommand cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":IDINVOICE", id);
                cmd.Parameters.AddRange(parameters1);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }
        public Invoice GetInvoice(string code)
        {
            Invoice a = new Invoice();
            OracleCommand cmd = null;
            string query = @"SELECT IDINVOICE,TOTAL FROM INVOICE WHERE CONTROLCODE=:CONTROLCODE";
            OracleDataReader dr = null;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                OracleParameter[] parameters1 = new OracleParameter[1];

                parameters1[0] = new OracleParameter(":CONTROLCODE", code);
                cmd.Parameters.AddRange(parameters1);

                dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    a.IdInvoide = int.Parse(dr[0].ToString());
                    a.Total=Convert.ToDouble(dr[1].ToString());
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
        public void UpdateTransaction(Invoice invoice)
        {
            string queryInvoice = @"INSERT INTO INVOIDEANULED(AMOUNT,MOTIVE,IDINVOICE)
VALUES(:AMOUNT,:MOTIVE,:IDINVOICE)";


            string queryDetail = @"UPDATE INVOICE SET STATUS='A' WHERE IDINVOICE=:IDINVOICE";
            string queryPayment = @"UPDATE PAYMENT SET PAIDOUT=PAIDOUT+:PAIDOUT, STATUS=1 WHERE IDPAYMENT=:IDPAYMENT";



            try
            {

                Debug.Listeners.Clear();
                Debug.Listeners.Add(Logss.LogInternalActivities);
                Debug.WriteLine(string.Format("{0} Info: Start Cancel Invoice{1}", DateTime.Now, Session.SessionCurrent + " Administraor"));
                Debug.Flush();
                DataTable payments = SelectPayments(invoice.IdInvoide);
                int pay=0;
                int i = 0;
                foreach (DataRow l in payments.Rows)
                {
                    pay++;
                }

                List<OracleCommand> cmds = DBImplementation.CreateNBasicCommands(2 + pay);
                List<OracleParameter[]> parameters = new List<OracleParameter[]>();
                cmds[0].CommandText = queryInvoice;
                cmds[0].Parameters.Add(new OracleParameter(":AMOUNT", invoice.Total));
                cmds[0].Parameters.Add(new OracleParameter(":MOTIVE", invoice.BussinesName));
                cmds[0].Parameters.Add(new OracleParameter(":IDINVOICE", invoice.IdInvoide));
                cmds[1].CommandText = queryDetail;
                cmds[1].Parameters.Add(new OracleParameter(":IDINVOICE", invoice.IdInvoide));
                foreach (DataRow l in payments.Rows)
                {
                    cmds[i].CommandText = queryPayment;
                    cmds[i].Parameters.Add(new OracleParameter(":PAIDOUT", Convert.ToDouble(l[1].ToString())));
                    cmds[i].Parameters.Add(new OracleParameter(":IDPAYMENT", int.Parse(l[0].ToString())));
                    i++;
                }
                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                Debug.Listeners.Clear();
                Debug.Listeners.Add(Logss.LogError);
                Debug.WriteLine(string.Format(string.Format("{0} | Error:  Could not Cancel Invoice({1}).", DateTime.Now, ex.Message)));
                Debug.Flush();
            }
        }

    }
}
