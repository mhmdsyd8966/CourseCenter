using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class SignUpDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string phoneNumber { get; set; }


        public Student ToStudent()
        {
            var student = new Student { FirstName = FirstName, LastName = LastName, Email = email, PhoneNumber = phoneNumber, UserName = email };

            return student;
        }
        public Admin ToAdmin()
        {
            var admin = new Admin { FirstName = FirstName, LastName = LastName, Email = email, PhoneNumber = phoneNumber, UserName = email };
            return admin;
        }
    }
}
