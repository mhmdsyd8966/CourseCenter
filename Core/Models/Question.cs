using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Question
    {
        public int Id { get; set; } 
        public string QuestionName {  get; set; }
        public string QuestionAnswer1 { get; set; }
        public string QuestionAnswer2 { get; set; }
        public string QuestionAnswer3 { get; set; }
        public string QuestionAnswer4 { get; set; }
        public string TrueAnswer { get; set; }
        [ForeignKey(nameof(Exams))]
        public int ExamsId { get; set; }    
        public Exams exams { get; set; }
    }
}
