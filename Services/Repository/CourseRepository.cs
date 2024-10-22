using Core;
using Core.Dto;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helpers;
using Services.IRepository;

namespace Services.Repository
{
    public class CourseRepository(CenterApplicationDbContext _context): ICourseRepository
    {
        public async Task<List<Course>> GetAllCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            if (courses == null)
                throw new ArgumentNullException("There isn't any course yet");
            return courses;
        }
        public async Task<bool> AddCourse(CourseDto dto)
        {
            var course = dto.CourseDtoToCourse();
            try
            {
                await _context.AddAsync(course);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<bool> UpdateCourse(int Id, CourseDto dto)
        {
            var course = await GetCourseById(Id);
            if (course == null)
                throw new ArgumentNullException("Course Doesn't exist");
            course.Name = dto.Name;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> DeleteCourse(int Id)
        {
            var course = await GetCourseById(Id);
            if (course == null)
                throw new ArgumentNullException("Course Doesn't exist");
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Course> GetCourseById(int Id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<bool> AddTeacherToCourse(int CourseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == CourseId);
            course.NumberofTeachers++;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
