using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public List<Teacher> Teachers { get; set; }
        public int NumberOfStudents { get; set; }
        public int NumberofTeachers { get; set; }
        public List<Student> students { get; set; }
    }
}
