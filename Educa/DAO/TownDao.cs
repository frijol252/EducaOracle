using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAO
{
    public interface TownDao : IDao<Town>
    {
        Town GET(string a, string b, string c);
    }
}
