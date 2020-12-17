using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Payment
    {
        int idPayment;
        double amount;
        double paidout;
        string detail;
        double balance;
        int status;
        public Payment()
        {

        }

        public int IdPayment { get => idPayment; set => idPayment = value; }
        public double Amount { get => amount; set => amount = value; }
        public double Paidout { get => paidout; set => paidout = value; }
        public string Detail { get => detail; set => detail = value; }
        public double Balance { get => balance; set => balance = value; }
        public int Status { get => status; set => status = value; }
    }
}
