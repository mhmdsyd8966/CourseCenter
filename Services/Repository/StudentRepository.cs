using Core;
using Core.Dto;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class StudentRepository(UserManager<Student> _studentManger, SignInManager<Student> _studentSignInManager,CenterApplicationDbContext _Context) : IStudentRepository
    {
        public async Task<SignInResult> Login(string email, string password,bool isPersistent)
        {
            var user =await _studentManger.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Account Doesn't Exist!");
            var result =await _studentSignInManager.PasswordSignInAsync(user, password, isPersistent, false);
            if(!result.Succeeded)
                throw new Exception("Email or password is invalid");
            return result;
        }

        public async Task SignOut()
        {
            await _studentSignInManager.SignOutAsync();
        }
        public async Task<bool> SignUp(SignUpDto dto)
        {
            var student = dto.ToStudent();
            var result = await _studentManger.CreateAsync(student, dto.password);
            await _studentManger.AddToRoleAsync(student, "Student");
            return true;
        }
        public async Task<bool> AddTeacherToStudent(string TeacherId,string StudentId)
        {
            
            var user = await _Context.Students.Include(x=>x.Teachers).Include(x=>x.Courses).FirstOrDefaultAsync(x=>x.Id==StudentId);
            var teacher =await _Context.Teachers.Include(x=>x.Course).FirstOrDefaultAsync(x=>x.Id==TeacherId);
            user.Teachers.Add(teacher);
            teacher.Course.NumberOfStudents++;
            if(!user.Courses.Contains(teacher.Course))
                user.Courses.Add(teacher.Course);


            var result = await _Context.SaveChangesAsync(); 
			return true;
        }

		public async Task<string> GetStudentId(string Name)
		{
			var user = await _Context.Students.Where(x=>x.Equals(Name)).FirstOrDefaultAsync();
            return user.Id;
		}
        public async Task<List<int>> GetCoursesId(string StudentId)
        {
            var user = await 
                _Context.Students.Include(x=>x.Courses).FirstOrDefaultAsync(x=>x.Id==StudentId);
            var courses = user.Courses;
            var CoursesId= courses.Select(x=>x.Id).ToList();
            return CoursesId;
        }
		public async Task<List<string>> GetTeachersId(string StudentId)
		{
			var user = await _Context.Students.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == StudentId);
			var Teachers = user.Teachers;
			var TeachersId = Teachers.Select(x => x.Id).ToList();
			return TeachersId;
		}
	}
}
