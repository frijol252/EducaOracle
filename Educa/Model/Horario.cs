using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Horario
    {
        #region Properiarities

        public short idHorario { get; set; }
        public string Day{ get; set; }
        public byte estado { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        DateTime updateDate { get; set; }
        byte status { get; set; }
        DateTime registrationDate { get; set; }
        #endregion


        #region constructores

        //insert
        public Horario(string day, DateTime horaInicio, DateTime horaFin)
        {
            this.Day = day;
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
        }
        //delete
        public Horario(short idHorario)
        {
            this.idHorario = idHorario;
        }
        //update
        public Horario(DateTime horaInicio, DateTime horaFin,string day, short idHorario)
        {
            this.idHorario = idHorario;
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
            this.Day = day;
        }
        //Select
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="idHorario"></param>
        /// <param name="horaInicio"></param>
        /// <param name="horaFin"></param>
        /// <param name="updateDate"></param>
        /// <param name="status"></param>
        /// <param name="registrationDate"></param>
        /// <param name="day"></param>
        public Horario(short idHorario,DateTime horaInicio, DateTime horaFin, DateTime updateDate, byte status, DateTime registrationDate, string day )
        {
            this.idHorario = idHorario;
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
            this.updateDate = updateDate;
            this.estado = status;
            this.registrationDate = registrationDate;
            this.Day = day;
        }
        #endregion
    }
}
