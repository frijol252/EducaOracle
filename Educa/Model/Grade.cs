using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Grade
    {
        int idGrado;
        double grade1;
        double grade2;
        double grade3;
        double grade4;
        double testgrade;
        double average;
        int idStudent;
        string student;
        int idClass;
        int idAverage;

        public Grade(int idGrado, double grade1, double grade2, double grade3, double grade4, double testgrade, double average, int idStudent, int idClass, int idAverage)
        {
            this.idGrado = idGrado;
            this.grade1 = grade1;
            this.grade2 = grade2;
            this.grade3 = grade3;
            this.grade4 = grade4;
            this.testgrade = testgrade;
            this.average = average;
            this.idStudent = idStudent;
            this.idClass = idClass;
            this.idAverage = idAverage;
        }
        public Grade(int idGrado, double grade1, double grade2, double grade3, double grade4, double testgrade, double average, int idTotal)
        {
            this.idGrado = idGrado;
            this.grade1 = grade1;
            this.grade2 = grade2;
            this.grade3 = grade3;
            this.grade4 = grade4;
            this.testgrade = testgrade;
            this.average = average;
            this.idAverage = idTotal;
        }



        #region geters
        public int IdGrado { get => idGrado; set => idGrado = value; }
        public double Grade1 { get => grade1; set => grade1 = value; }
        public double Grade2 { get => grade2; set => grade2 = value; }
        public double Grade3 { get => grade3; set => grade3 = value; }
       public double Grade4 { get => grade4; set => grade4 = value; }
        public double Testgrade { get => testgrade; set => testgrade = value; }
        public double Average { get => average; set => average = value; }
        public int IdStudent { get => idStudent; set => idStudent = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public int IdAverage { get => idAverage; set => idAverage = value; }
        public string Student { get => student; set => student = value; }
        #endregion
    }
}
