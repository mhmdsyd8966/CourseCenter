using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class ExamDto
    {
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public List<Question> Questions { get; set; }
    }
    public class ApplyExamDto
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public List<QuestionsAnswer> answers { get; set; }
    }
    public class QuestionsAnswer
    {
        public int QuestionId { get; set;}
        public string Answer { get; set; }
    }
    public class ExamQuestionDto
    {
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionAnswer1 { get; set; }
        public string QuestionAnswer2 { get; set; }
        public string QuestionAnswer3 { get; set; }
        public string QuestionAnswer4 { get; set; }
        public string TrueAnswer { get; set; }
    }
    }
    

