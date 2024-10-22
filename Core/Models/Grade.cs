using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int grade { get; set; }
        [ForeignKey(nameof(exam))]
        public int ExamId { get; set; }
        public Exams exam { get; set; }
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
        public Student Student { get; set; }

    }
}
