using Core.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
    public interface IStudentRepository
    {
        Task<SignInResult> Login(string email, string password, bool isPersistent);
        Task<bool> SignUp(SignUpDto dto);
        Task SignOut();
        Task<bool> AddTeacherToStudent(string TeacherId, string StudentId);
        Task<string> GetStudentId(string Id);
        Task<List<int>> GetCoursesId(string StudentId);
         Task<List<string>> GetTeachersId(string StudentId);

	}
}
