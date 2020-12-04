using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        private int userID;
        private string userName;
        private string password;
        private string role;
        private byte state;
        private DateTime createDate;
        private DateTime lastUpdate;
        private int stat;
        private int personId;
        public string Password { get => password; set => password = value; }
        public string UserName { get => userName; set => userName = value; }
        public int PersonId { get => personId; set => personId = value; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public byte State { get => state; set => state = value; }
        public string Role { get => role; set => role = value; }
        public int UserID { get => userID; set => userID = value; }
        public int Stat { get => stat; set => stat = value; }

        //GET
        public User(int userID, string userName, string password, string role, byte state, DateTime createDate, DateTime lastUpdate, int personId,int stat)
        {
            this.userID= userID;
            this.userName = userName;
            this.password= password;
            this.role = role;
            this.state = state;
            this.createDate = createDate;
            this.lastUpdate = lastUpdate;
            this.personId = personId;
            this.stat = stat;
        }
        //INSERT
        public User(string userName, string password, string role,int personid)
        {
            this.userName = userName;
            this.password = password;
            this.role = role;
            this.personId = personid;
        }
        //UPDATE password
        public User( string password, int userID)
        {
            this.password = password;
            this.userID = userID;
        }
    }
}
