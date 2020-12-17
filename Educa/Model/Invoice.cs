using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Invoice
    {
        int idInvoide;
        double total;
        int nroInvoice;
        string controlCode;
        string status;
        string nit;
        string bussinesName;
        int idDosage;
        int idPayer;
        public Invoice()
        {

        }

        public int IdInvoide { get => idInvoide; set => idInvoide = value; }
        public double Total { get => total; set => total = value; }
        public int NroInvoice { get => nroInvoice; set => nroInvoice = value; }
        public string ControlCode { get => controlCode; set => controlCode = value; }
        public string Status { get => status; set => status = value; }
        public string Nit { get => nit; set => nit = value; }
        public string BussinesName { get => bussinesName; set => bussinesName = value; }
        public int IdDosage { get => idDosage; set => idDosage = value; }
        public int IdPayer { get => idPayer; set => idPayer = value; }
    }
}
