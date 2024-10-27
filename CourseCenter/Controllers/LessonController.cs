using Core.Dto;
using Core.Models;
using CourseCenter.Helpers;
using CourseCenter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.IRepository;

namespace CourseCenter.Controllers
{
	public class LessonController(ILessonRepository _lesson,ITeacherRepository _teacher,IStudentRepository _student,UserManager<Teacher> _teacherManger,UserManager<Student> _studentManger) : Controller
	{
		public async Task<IActionResult> GetLessons(string Id)
		{
			try
			{
				var lessons = await _lesson.GetLessonsByTeacherId(Id);
				var user = User;
				if (User.IsInRole("Teacher"))
				{

					var teacherId = _teacherManger.GetUserId(User);
					ViewBag.TeacherId = teacherId;
					ViewBag.Id = Id;
				}
				if (User.IsInRole("Student"))
				{
					var studentId = _studentManger.GetUserId(User);
					var teachers = await _student.GetTeachersId(studentId);
					foreach(var teacher in teachers)
					{
						if(lessons.First().TeacherId == teacher)
							ViewBag.teacher = true;
					}
					ViewBag.StudentId = studentId;
				}

				return View(lessons);
			}
			catch(Exception ex)
			{
				ViewBag.Error=ex.Message;
				return View();
			}
			
		}
		[Authorize(Roles ="Teacher")]
		public async Task<IActionResult> AddLesson()
		{
			return View();
		}
		[Authorize(Roles ="Teacher")]
		[HttpPost]
		public async Task<IActionResult> AddLesson(AddLessonDto dto)
		{
			try
			{
				var lesson = dto.AddLessonDtoToLessonDto(dto);
				lesson.Photo=UploadPhoto.UploadImage("Lesson",dto.Photo);
				var Id = _teacherManger.GetUserId(User);
				lesson.TeacherId = Id;
				await _lesson.AddLesson(lesson);
                return Json(new
                {
                    Success = true,
                    Message = "Lesson Added Successfully"
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
		public async Task<IActionResult> GetLesson(int id)
		{
			try
			{
				var lesson = await _lesson.GetLessonById(id);
				return View(lesson);
			}catch (Exception ex)
			{
				var teacherId = _teacherManger.GetUserId(User);
				var lessons = await _lesson.GetLessonsByTeacherId(teacherId);
				ViewBag.Error = ex.Message;
				return View("GetLessons",lessons);
			}
		}

		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> UpdateLesson(int id)
		{
			try
			{
				ViewBag.Id = id;
				return View();
			}catch(Exception ex)
			{
				var teacherId = _teacherManger.GetUserId(User);
				var lessons = await _lesson.GetLessonsByTeacherId(teacherId);
				ViewBag.Error = ex.Message;
				return View("GetLessons",lessons);
			}
		}
		[HttpPut]
		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> UpdateLesson(int id, AddLessonDto dto)
		{
			try
			{
				var lesson = await _lesson.GetLessonById(id);
				var photo = UploadPhoto.UploadImage("Lesson",dto.Photo);
				var updatelesson = dto.AddLessonDtoToUpdateLessonDto(dto);
				if (photo!= lesson.Photo)
				{
					var filePath = $"D:/CourseCenter/CourseCenter/wwwroot/" + lesson.Photo;
					if (System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath);
					}
				}
				updatelesson.Photo=photo;
				await _lesson.UpdateLesson(id, updatelesson);
                return Json(new
                {
                    Success = true,
                    Message = "Lesson Updated Successfully"
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
		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> DeleteLesson(int lessonId)
		{
			try
			{
				await _lesson.DeleteLessonById(lessonId);
                return Json(new
                {
                    Success = true,
                    Message = "lesson Deleted Successfully"
                });
            }
            catch(Exception ex)
			{
				var teacherId = _teacherManger.GetUserId(User);
				var lessons = await _lesson.GetLessonsByTeacherId(teacherId);
				ViewBag.Error = ex.Message;
                return Json(new
                {
                    Success = false,
                    Message =ex.Message
                });
            }
		}
		[HttpDelete]
		[Authorize(Roles ="Teacher")]
		public async Task<IActionResult> DeleteAllLessons(string TeacherId)
		{
			try{
				await _lesson.DeleteLessonsByTeachrId(TeacherId);
                return Json(new
                {
                    Success = true,
                    Message = "All Lessons Deleted Successfully"
                });
            }
            catch(Exception ex)
			{
				ViewBag.Error = ex.Message;
                return Json(new
                {
                    Success = true,
                    Message = ex.Message
                });
            }
		}
	}
}
