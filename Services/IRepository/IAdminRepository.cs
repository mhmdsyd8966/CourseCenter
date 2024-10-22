using Core.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
    public interface IAdminRepository
    {
        Task<SignInResult> Login(string email, string password, bool isPersistent);
        Task<bool> SignUp(SignUpDto dto);
        Task SignOut();



    }
}
