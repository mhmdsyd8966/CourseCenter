using Core.Dto;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourses();
        Task<bool> UpdateCourse(int Id, CourseDto dto);
        Task<bool> AddCourse(CourseDto dto);
        Task<bool> DeleteCourse(int Id);
        Task<Course> GetCourseById(int Id);
        Task<bool> AddTeacherToCourse(int CourseId);
    }
}
