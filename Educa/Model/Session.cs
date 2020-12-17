using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Session
    {
        private static int sessionID;
        private static string sessionUser="";
        private static string sessionRole;
        private static string sessionCurrent="";
        private static string sessionEmail;
        private static string sessionphoto;
        private static string sessionstat;

        public static int SessionID { get => sessionID; set => sessionID = value; }
        public static string SessionUser { get => sessionUser; set => sessionUser = value; }
        public static string SessionRole { get => sessionRole; set => sessionRole = value; }
        public static string SessionCurrent { get => sessionCurrent; set => sessionCurrent = value; }
        public static string SessionEmail { get => sessionEmail; set => sessionEmail = value; }
        public static string Sessionphoto { get => sessionphoto; set => sessionphoto = value; }
        public static string Sessionstat { get => sessionstat; set => sessionstat = value; }

        public static string UserName()
        {
            return "Usuario: " + SessionUser + ", Rol: " + SessionRole;
        }
    }
}
