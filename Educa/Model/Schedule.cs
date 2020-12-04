using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Schedule
    {
        int id;
        string start;
        string finish;
        string day;

        public Schedule(int id, string start, string finish, string day)
        {
            this.id = id;
            this.start = start;
            this.finish = finish;
            this.day = day;
        }

        public int Id { get => id; set => id = value; }
        public string Start { get => start; set => start = value; }
        public string Finish { get => finish; set => finish = value; }
        public string Day { get => day; set => day = value; }
    }
}
