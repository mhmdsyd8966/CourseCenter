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
    public class LessonRepository(CenterApplicationDbContext _context):ILessonRepository
    {
        public async Task<List<Lesson>> GetLessonsByTeacherId(string Id)
        {
            var lessons = await _context.Lessons.Include(x => x.Teacher).Where(x=>x.TeacherId==Id).ToListAsync();
            if (lessons == null)
                throw new NullReferenceException("There is no Lessons yet");
            return lessons;
        }
        public async Task<bool> DeleteLessonById(int Id)
        {
            var lesson = await GetLessonById(Id);
            _context.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Lesson> GetLessonById(int Id)
        {
            if (Id == null)
                throw new ArgumentNullException("id can't be empty");
            else if ( Id <= 0)
                throw new Exception("Id Can't smaller than 1");
            else
            {
				var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == Id);
				if (lesson == null)
					throw new NullReferenceException("lesson doen't exist");
				return lesson;
			}
             
        }
        public async Task<bool> DeleteLessonsByTeachrId(string Id)
        {
            await _context.Lessons.Include(x=>x.Teacher).Where(x => x.TeacherId == Id).ExecuteDeleteAsync();
            return true;
        }
        public async Task<Lesson> AddLesson(LessonDto dto)
        {
            var lesson = dto.LessonDtoToLesson();
            await _context.AddAsync(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }
        public async Task<bool> UpdateLesson(int Id,UpdateLessonDto dto)
        {
            var lesson =await GetLessonById(Id);
            lesson.Name = dto.Name;
            lesson.VideoLink = dto.VideoLink;
            lesson.Description= dto.Description;
            lesson.Photo = dto.Photo;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
