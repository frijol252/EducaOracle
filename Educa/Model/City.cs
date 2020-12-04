using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class City
    {
        private byte cityid;
        private string cityName;
        private byte status;

        public City(byte cityid, string cityName, byte status)
        {
            this.cityid = cityid;
            this.cityName = cityName;
            this.status = status;
        }

        public byte Cityid { get => cityid; set => cityid = value; }
        public string CityName { get => cityName; set => cityName = value; }
        public byte Status { get => status; set => status = value; }
    }
}
