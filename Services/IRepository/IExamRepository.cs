using Core.Dto;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
    public interface IExamRepository
    {
        Task<List<Exams>> GetAllExamsByTeacherId(string Id);
        Task<Exams> GetExamById(int id);
        Task<bool> DeleteExamById(int id);
        Task<bool> DeleteAllExamsByTeacherId(string Id);
        Task<Exams> AddExam(ExamDto dto);
        Task<Grade> ApplyToExam(ApplyExamDto dto);
        Task<bool> UpdateExamQuestion(ExamQuestionDto dto);
    }
}
