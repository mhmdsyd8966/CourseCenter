using Core.Dto;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class AdminRepository(UserManager<Admin> _adminManger, SignInManager<Admin> _adminSignInManager):IAdminRepository
    {
        public async Task<SignInResult> Login(string email, string password, bool isPersistent)
        {
            var user = await _adminManger.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Account Doesn't Exist!");
            var result = await _adminSignInManager.PasswordSignInAsync(user, password, isPersistent, false);
            if (!result.Succeeded)
                throw new Exception("Email or password is invalid");
            return result;
        }
        public async Task<bool> SignUp(SignUpDto dto)
        {
            var Admin = dto.ToAdmin();
            var result = await _adminManger.CreateAsync(Admin, dto.password);
            await _adminManger.AddToRoleAsync(Admin, "Admin");
            return true;
        }
        public async Task SignOut()
        {
            await _adminSignInManager.SignOutAsync();
        }
    }
}
