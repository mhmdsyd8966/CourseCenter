using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Teacher:IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name =>FirstName + " " + LastName;
        public string Photo { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }   
        public Course Course { get; set; }
        public List<Exams> Exams { get; set; }
        public List<Student> Students { get; set; }
    }
}
