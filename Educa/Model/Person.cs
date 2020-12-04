using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        #region Properiarities
        int personId;
        string names;
        string lastName;
        string secondLastName;
        string address;
        string phone;
        DateTime birthDate;
        string gender;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;
        DateTime startDate;
        DateTime finishDate;
        string email;
        double latitude;
        double longitude;
        byte townId;
        string photo;
        string pathImage;
        string pathrevision;


        #endregion
        #region geters

        public int PersonId { get => personId; set => personId = value; }
        public string Names { get => names; set => names = value; }
        public string SecondLastName { get => secondLastName; set => secondLastName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime FinishDate { get => finishDate; set => finishDate = value; }
        public string Email { get => email; set => email = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public byte TownId { get => townId; set => townId = value; }
        public string Photo { get => photo; set => photo = value; }
        public string PathImage { get => pathImage; set => pathImage = value; }
        public string Pathrevision { get => pathrevision; set => pathrevision = value; }




        #endregion
        #region constructores
        public Person(int personId, string names, string lastName, string secondLastName, string address, string phone, DateTime birthDate, string gender, byte status, DateTime registrationDate, DateTime updateDate, DateTime startDate, DateTime finishDate, string email, double latitude, double longitude, byte townId, string photo, string pathImage, string pathrevision)
        {
            this.personId = personId;
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.phone = phone;
            this.birthDate = birthDate;
            this.gender = gender;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.email = email;
            this.latitude = latitude;
            this.longitude = longitude;
            this.townId = townId;
            this.photo = photo;
            this.pathImage = pathImage;
            this.pathrevision = pathrevision;
        }
        public Person(int personId, string names, string lastName, string secondLastName, string address, string phone, string email, string photo, string pathImage, string pathrevision)
        {
            this.personId = personId;
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.photo = photo;
            this.pathImage = pathImage;
            this.pathrevision = pathrevision;
        }
        //GET
        public Person(int personId, string names, string lastName, string secondLastName, string address, string phone, DateTime birthDate, string gender, byte status, DateTime registrationDate, DateTime updateDate, DateTime startDate, DateTime finishDate, string email, double latitude, double longitude, byte townId, string photo)
        {
            this.personId = personId;
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.phone = phone;
            this.birthDate = birthDate;
            this.gender = gender;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.email = email;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.townId = townId;
            this.photo = photo;
        }

        public Person(string email,int person)
        {
            this.email = email;
            this.personId = person;
        }
        #endregion

    }
}
