using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Exams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set;}
        public List<Student> Students { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }
        public List<Grade> Grades { get; set; }

    }
}
