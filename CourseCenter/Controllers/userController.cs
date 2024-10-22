using Core.Dto;
using CourseCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.IRepository;
using Services.Repository;
using CourseCenter.Helpers;
using Microsoft.AspNetCore.Identity;
using Core.Models;

namespace CourseCenter.Controllers
{
    public class userController(
        IStudentRepository _student
        ,UserManager<Teacher> _teacherManger
        , ITeacherRepository _teacher
        , IAdminRepository _admin
        , ICourseRepository _course
        ,UserManager<Student> _studentManger) : Controller
    {

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (login.Role == "Student")
                {
                    var result = await _student.Login(login.Email, login.Password, login.IspPresistant);

                }

                else if (login.Role == "Admin")
                    await _admin.Login(login.Email, login.Password, login.IspPresistant);
                else if (login.Role == "Teacher")
                    await _teacher.Login(login.Email, login.Password, login.IspPresistant);
                return RedirectToAction("Index", "Home");
            } catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();


            }

        }
        public async Task<IActionResult> LogOut()
        {
            await _student.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {

            var result = await _student.SignUp(dto);
            if (result == true)
                return RedirectToAction("SignIn");
            return View();
        }
        public IActionResult SignUpAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAdmin(SignUpDto dto)
        {
            var result = await _admin.SignUp(dto);
            if (result == true)
                return RedirectToAction("SignIn");
            return View();
        }
        public async Task<IActionResult> SignUpTeacher()
        {
            var courses = await _course.GetAllCourses();
            ViewBag.Courses = courses;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SignUpTeacher(SignUpTeacherDto dto)
        {
            try
            {
                var photo = UploadPhoto.UploadImage("User",dto.Photo);
                var SignUpDto = dto.ToTeacherSignUpDto();
                SignUpDto.Photo = photo;
                var result = await _teacher.SignUp(SignUpDto);
                if (result == true)
                {
                    await _course.AddTeacherToCourse(dto.CourseId);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            } catch (Exception ex)
            {
				var courses = await _course.GetAllCourses();
				ViewBag.Courses = courses;
				ViewBag.Error = ex.Message;
                return View();

            }
        }
       
        
        public async Task<IActionResult> GetAllTeachers()
        {
			var teachers = await _teacher.GetAllTeachers();
            
			if (User.IsInRole("Teacher"))
			{
				ViewBag.Teacher = _teacherManger.GetUserId(User);
			}
            if (User.IsInRole("Student"))
            {
                var student = _studentManger.GetUserId(User);
                var teachersForStudent=await  _student.GetTeachersId(student);
                ViewBag.Teachers = teachersForStudent;

            }

			if (teachers == null)
            {
                ViewBag.Error = "There is no teachers yet";
                return View();
            }
            return View(teachers);

        }
        [HttpPost]
        public async Task<IActionResult> AddTeacherToStudent(string TeacherId)
        {
            try
            {
                var user = _studentManger.GetUserId(User);
				await _student.AddTeacherToStudent(TeacherId, user);
				return RedirectToAction("GetAllTeachers");
			}
			catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(string teacherId)
        {
            try
            {
                await _teacher.DeleteTeacher(teacherId);
                return RedirectToAction("GetAllTeachers");
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
        }
    }

