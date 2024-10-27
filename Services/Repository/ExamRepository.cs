using Core;
using Core.Dto;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ExamRepository(CenterApplicationDbContext _context): IExamRepository
    {
        public async Task<List<Exams>> GetAllExamsByTeacherId(string Id)
        {
            if(Id == null)
                throw new ArgumentNullException("Id Can't be null");
            var exams = await _context.Exams.Where(x => x.TeacherId == Id).ToListAsync();
            if (exams == null)
                throw new ArgumentNullException("There is no exams yet");
            return exams;
        }
        public async Task<Exams> GetExamById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("Id Can't be less than 1");
            var exam = await _context.Exams.FirstOrDefaultAsync(x=>x.Id==id);
            if (exam == null)
                throw new ArgumentNullException("There No Exam with this id");
            return exam;
        }
        public async Task<bool> DeleteExamById(int id)
        {
            var exam = await GetExamById(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAllExamsByTeacherId(string Id)
        {
            if(Id==null)
                throw new ArgumentNullException("Id Can't be null");
            await _context.Exams.Where(x=>x.TeacherId==Id).ExecuteDeleteAsync();
            return true;  
        }
        public async Task<Exams> AddExam(ExamDto dto)
        {
            var exam = dto.ExamDtoToExam();
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
            return exam;
        }
        public async Task<Grade> ApplyToExam(ApplyExamDto dto)
        {
            var exam = await _context.Exams.Include(x => x.Questions).FirstOrDefaultAsync(x => x.Id == dto.ExamId);
            var Questions = exam.Questions.ToList();
            var grade = dto.ExamToGrade(Questions);
            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();
            return grade;

        }
        public async Task<bool> UpdateExamQuestion(ExamQuestionDto dto)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x=>x.Id == dto.QuestionId);
            question.QuestionAnswer1= dto.QuestionAnswer1;
            question.QuestionAnswer2= dto.QuestionAnswer2;
            question.QuestionAnswer3= dto.QuestionAnswer3;
            question.QuestionName= dto.QuestionName;
            question.QuestionAnswer4= dto.QuestionAnswer4;
            question.TrueAnswer= dto.TrueAnswer;
            await _context.SaveChangesAsync();
            return true;
        }
        

        
    }
}
