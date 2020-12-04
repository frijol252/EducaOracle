using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Class
    {
        int idClass;
        int teacherId;
        int matterId;
        int courseId;
        #region Construct
        public Class(int idClass, int teacherId, int matterId, int courseId)
        {
            this.idClass = idClass;
            this.teacherId = teacherId;
            this.matterId = matterId;
            this.courseId = courseId;
        }
        public Class(int idClass)
        {
            this.idClass = idClass;
        }
        #endregion

        #region gets
        public int IdClass { get => idClass; set => idClass = value; }
        public int TeacherId { get => teacherId; set => teacherId = value; }
        public int MatterId { get => matterId; set => matterId = value; }
        public int CourseId { get => courseId; set => courseId = value; }
        #endregion
    }
}
