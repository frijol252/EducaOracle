using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Town
    {
        byte id=1;
        string name;
        byte state;
        byte pid;

        public Town(byte id, string name, byte state, byte pid)
        {
            this.id = id;
            this.name = name;
            this.state = state;
            this.pid = pid;
        }

        public Town(byte id)
        {
            this.id = id;
        }

        public byte Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public byte State { get => state; set => state = value; }
        public byte Pid { get => pid; set => pid = value; }
    }
}
