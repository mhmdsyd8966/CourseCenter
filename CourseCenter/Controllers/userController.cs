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
                return Json(new
                {
                    Success = true,
                    Message = "Successfully Logged In"
                });
            } catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });


            }

        }
        public async Task<IActionResult> LogOut()
        {
            await _student.SignOut();
            return RedirectToAction("Index","Home");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {
            try
            {
                var result = await _student.SignUp(dto);

                    return Json(new
                    {
                        Success = true,
                        Message = "Successfully signed Up"
                    });
            }catch(Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message =ex.Message
            });
            }
            
            
        }
        public IActionResult SignUpAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAdmin(SignUpDto dto)
        {
            try
            {
                var result = await _admin.SignUp(dto);
                if (result == true)
                    return Json(new
                    {
                        Success = true,
                        Message = "Signed Up A New Admin Successfully"
                    });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            
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
                    await _course.AddTeacherToCourse(dto.CourseId);
                    return Json(new
                    {
                        Success = true,
                        Message = "Successfully Added new Teacher"
                    });
                
                
            } catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });

            }
        }
       
        
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _teacher.GetAllTeachers();

                if (User.IsInRole("Teacher"))
                {
                    ViewBag.Teacher = _teacherManger.GetUserId(User);
                }
                if (User.IsInRole("Student"))
                {
                    var student = _studentManger.GetUserId(User);
                    var teachersForStudent = await _student.GetTeachersId(student);
                    ViewBag.CheckTeachers = teachersForStudent;

                }

                if (teachers == null)
                {
                    ViewBag.Error = "There is no teachers yet";
                    return View();
                }
                ViewBag.Teachers = teachers;
                return View();
            }catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
			

        }
        [HttpPost]
        public async Task<IActionResult> AddTeacherToStudent(string TeacherId)
        {
            try
            {
                var user = _studentManger.GetUserId(User);
				await _student.AddTeacherToStudent(TeacherId, user);
                return Json(new
                {
                    Success = true,
                    Message = "Student Signed with this Teacher Successfully"
                });
            }
			catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(string teacherId)
        {
            try
            {
                await _teacher.DeleteTeacher(teacherId);
                return Json(new
                {
                    Success = true,
                    Message = "Successfully Deleted This Teacher"
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }
        }
    }

