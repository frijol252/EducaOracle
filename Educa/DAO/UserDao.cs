using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAO
{
    public interface UserDao : IDao<User>
    {
        DataTable Login(string userName, string password);
        User Get(int id);
    }
}
