using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Subject
    {
        int idMatter;
        string matterName;
        int categoryMatter;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;
        #region constructs
        public Subject(int idMatter, string matterName, int categoryMatter, byte status, DateTime registrationDate, DateTime updateDate)
        {
            this.idMatter = idMatter;
            this.matterName = matterName;
            this.categoryMatter = categoryMatter;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
        }
        //grid
        public Subject(int idMatter, string matterName, int categoryMatter)
        {
            this.idMatter = idMatter;
            this.matterName = matterName;
            this.categoryMatter = categoryMatter;
        }
        #endregion
        #region Gets
        public int IdMatter { get => idMatter; set => idMatter = value; }
        public string MatterName { get => matterName; set => matterName = value; }
        public int CategoryMatter { get => categoryMatter; set => categoryMatter = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        #endregion
    }
}
