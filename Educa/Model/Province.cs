using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Province
    {
        byte id;
        string name;
        byte state;
        byte cityId;

        public Province(byte id, string name, byte state, byte cityId)
        {
            this.id = id;
            this.name = name;
            this.state = state;
            this.cityId = cityId;
        }

        public byte Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public byte State { get => state; set => state = value; }
        public byte CityId { get => cityId; set => cityId = value; }
    }
}
