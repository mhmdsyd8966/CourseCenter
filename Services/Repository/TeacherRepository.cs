using Core;
using Core.Dto;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class TeacherRepository(UserManager<Teacher> _teacherManger, SignInManager<Teacher> _teacherSignInManager,CenterApplicationDbContext _context): ITeacherRepository
    {
        public async Task<SignInResult> Login(string email, string password, bool isPersistent)
        {
            var user = await _teacherManger.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Account Doesn't Exist!");
            var result = await _teacherSignInManager.PasswordSignInAsync(user, password, isPersistent, false);
            if (!result.Succeeded)
                throw new Exception("Email or password is invalid");
            return result;
        }

        public async Task SignOut()
        {
            await _teacherSignInManager.SignOutAsync();
        }
        public async Task<bool> SignUp(TeacherSignupDto dto)
        {
            var teacher = dto.ToTeacher();
            var result = await _teacherManger.CreateAsync(teacher, dto.password);
            await _teacherManger.AddToRoleAsync(teacher, "Teacher");
            return true;
        }
        public async Task<List<Teacher>> GetAllTeachers()
        {
            try
            {
                var teachers = await _context.Teachers.Include(x => x.Course).ToListAsync();
                return teachers;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<List<Teacher>> GetTeachersByName(string name)
        {
            try
            {
                var teachers = await _context.Teachers.Where(x => x.FirstName.Contains(name)|| x.LastName.Contains(name)).ToListAsync();
				return teachers;
			}
			catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<bool> DeleteTeacher(string teacherId)
        {
            try
            {
                var teacher = _context.Teachers.FirstOrDefault(x=>x.Id == teacherId);
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
