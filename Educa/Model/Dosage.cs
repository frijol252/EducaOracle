using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Dosage
    {
        int idDosage;
        long nroAuthorization;
        DateTime deadLine;
        string dosageKey;
        int initialNumber;
        int finalNumber;

        public int IdDosage { get => idDosage; set => idDosage = value; }
        public long NroAuthorization { get => nroAuthorization; set => nroAuthorization = value; }
        public DateTime DeadLine { get => deadLine; set => deadLine = value; }
        public string DosageKey { get => dosageKey; set => dosageKey = value; }
        public int InitialNumber { get => initialNumber; set => initialNumber = value; }
        public int FinalNumber { get => finalNumber; set => finalNumber = value; }

        public Dosage()
        {
        }
    }
}
