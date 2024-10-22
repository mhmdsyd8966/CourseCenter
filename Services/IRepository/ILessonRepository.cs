using Core.Dto;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
    public interface ILessonRepository
    {
        Task<List<Lesson>> GetLessonsByTeacherId(string Id);
        Task<Lesson> GetLessonById(int Id);
        Task<bool> DeleteLessonById(int Id);
        Task<bool> DeleteLessonsByTeachrId(string Id);
        Task<Lesson> AddLesson(LessonDto dto);
        Task<bool> UpdateLesson(int Id, UpdateLessonDto dto);
    }
}
