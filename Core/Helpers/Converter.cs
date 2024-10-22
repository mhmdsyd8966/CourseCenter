using Core.Dto;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class Converter
    {
        public static Course CourseDtoToCourse(this CourseDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                NumberOfStudents = 0,
                NumberofTeachers = 0,
                students = new List<Student>(),
                Teachers = new List<Teacher>()
            };
            return course;
        }
        public static Lesson LessonDtoToLesson(this LessonDto dto)
        {
            var lesson = new Lesson
            {
                Name = dto.Name,
                Description = dto.Description,
                Photo = dto.Photo,
                TeacherId = dto.TeacherId,
                VideoLink = dto.VideoLink,
            };
            return lesson;
        }
        public static Exams ExamDtoToExam(this ExamDto dto)
        {
            var exam = new Exams
            {
                Questions = dto.Questions,
                Name = dto.Name,
                NumberOfQuestions = dto.Questions.Count,
                NumberOfUsers = 0,
                Students = new List<Student>(),
                TeacherId = dto.TeacherId,

            };
            return exam;
        }
        public static Grade ExamToGrade(this ApplyExamDto dto,List<Question> Questions)
        {
            var gradeNum = 0;

            foreach(var Question in Questions)
            {
                foreach (var answer in dto.answers)
                {
                    if(answer.Equals( Question.TrueAnswer))
                        gradeNum++;
                }
            }
            var grade = new Grade
            {
                ExamId = dto.ExamId,
                grade = gradeNum,
                StudentId = dto.StudentId,
            };
            return grade;
        }
      
        
    }
}
