using Core.Dto;
using System.ComponentModel.DataAnnotations;

namespace CourseCenter.Models
{
    public class SignUpTeacherDto()
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Photo { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int CourseId { get; set; }
        public TeacherSignupDto ToTeacherSignUpDto()
        {
            
            var TeacherSignupDto = new TeacherSignupDto();
            TeacherSignupDto.FirstName = FirstName;
            TeacherSignupDto.LastName = LastName;
            TeacherSignupDto.phoneNumber = phoneNumber;
            TeacherSignupDto.email = email;
            TeacherSignupDto.password = password;
            TeacherSignupDto.CourseId = CourseId;
            return TeacherSignupDto;


    }
    }
    
}
