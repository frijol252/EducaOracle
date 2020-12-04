using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student
    {
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


        int studentId;
        string rudeNumber;
        int idCourse;

        #region Constructores
        public Student(int personId, string names, string lastName, string secondLastName, string address, string phone, DateTime birthDate, string gender, byte status, DateTime registrationDate, DateTime updateDate, DateTime startDate, DateTime finishDate, string email, double latitude, double longitude, byte townId, string photo, string pathImage, string pathrevision, int studentId, string rudeNumber, int idCourse)
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
            this.studentId = studentId;
            this.rudeNumber = rudeNumber;
            this.idCourse = idCourse;
        }


        //INSERT
        public Student( string names, string lastName, string secondLastName, string address, string phone, DateTime birthDate, string gender, string email, double latitude, double longitude, byte townId, string photo, string rudeNumber,int idCourse)
        {
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.phone = phone;
            this.birthDate = birthDate;
            this.gender = gender;
            this.email = email;
            this.latitude = latitude;
            this.longitude = longitude;
            this.townId = townId;
            this.photo = photo;
            this.rudeNumber = rudeNumber;
            this.idCourse = idCourse;
        }

        //UPDATE
        public Student(int id,string names, string lastName, string secondLastName, string address, string phone, string email, double latitude, double longitude, byte townId, string photo, string rudeNumber, int idCourse,string pathImage,int student)
        {
            this.personId = id;
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.latitude = latitude;
            this.longitude = longitude;
            this.townId = townId;
            this.photo = photo;
            this.rudeNumber = rudeNumber;
            this.idCourse = idCourse;
            this.pathImage = pathImage;
            this.studentId = student;
        }
        //DELETE
        public Student(int i)
        {
            this.personId = i;
        }


        public Student(int personId, string names, string lastName, string secondLastName, string address, string phone, DateTime birthDate, string gender, byte status, DateTime registrationDate, DateTime updateDate, DateTime startDate, DateTime finishDate, string email, double latitude, double longitude, byte townId, string photo, string rudeNumber, int idstudent)
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
            this.rudeNumber = rudeNumber;
            this.studentId = idstudent;
        }
        public Student(int personId, string names, string lastName, string secondLastName, string address, string phone, DateTime birthDate, string gender, byte status, DateTime registrationDate, DateTime updateDate, DateTime startDate, DateTime finishDate, string email, byte townId, string photo, string rudeNumber)
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
            this.townId = townId;
            this.photo = photo;
            this.rudeNumber = rudeNumber;
        }
        #endregion
        #region Getters
        public int PersonId { get => personId; set => personId = value; }
        public string Names { get => names; set => names = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string SecondLastName { get => secondLastName; set => secondLastName = value; }
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
        public int StudentId { get => studentId; set => studentId = value; }
        public string RudeNumber { get => rudeNumber; set => rudeNumber = value; }
        public int IdCourse { get => idCourse; set => idCourse = value; }
        #endregion
    }
}
