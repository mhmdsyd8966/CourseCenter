using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class TeacherSignupDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int CourseId { get; set; }
        public Teacher ToTeacher()
        {
            var teacher = new Teacher { FirstName = FirstName, LastName = LastName, Email = email, PhoneNumber = phoneNumber, UserName = email,Photo=Photo,CourseId=CourseId };

            return teacher;
        }
    }
}
