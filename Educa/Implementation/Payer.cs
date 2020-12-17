using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Implementation
{
    public class Payer
    {
        int idPayer=-1;
        string nit = null;
        string businessName = null;
        public Payer()
        {

        }

        public int IdPayer { get => idPayer; set => idPayer = value; }
        public string Nit { get => nit; set => nit = value; }
        public string BusinessName { get => businessName; set => businessName = value; }
    }
}
