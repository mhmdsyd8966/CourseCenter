using Core.Dto;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
    public interface ITeacherRepository
    {
        Task<SignInResult> Login(string email, string password, bool isPersistent);
        Task SignOut(); 
        Task<bool> SignUp(TeacherSignupDto dto);
        Task<List<Teacher>> GetAllTeachers();
        Task<List<Teacher>> GetTeachersByName(string name);
        Task<bool> DeleteTeacher(string teacherId);

    }
}
