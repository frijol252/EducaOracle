using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAO
{
    public interface HorarioDao : IDao<Horario>
    {
        Horario Get(byte id);
        //PWDENCRYPT('123456') encriptar
        //PWDCOMPARE('123456', contrasenia)
    }
}
