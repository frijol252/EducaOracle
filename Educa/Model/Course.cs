using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;


namespace Model
{
    public class Course
    {
        int idcourse;
        int number;
        string letter;
        string section;


        public Course(int idcourse, int number, string letter, string section)
        {
            this.idcourse = idcourse;
            this.number = number;
            this.letter = letter;
            this.section = section;
        }



        public int Idcourse { get => idcourse; set => idcourse = value; }
        public int Number { get => number; set => number = value; }
        public string Letter { get => letter; set => letter = value; }
        public string Section { get => section; set => section = value; }
    }
}
