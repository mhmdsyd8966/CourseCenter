using Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IRepository;

namespace CourseCenter.Controllers
{
    public class CourseController(ICourseRepository _course) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Courses = await _course.GetAllCourses();
            
            return View(Courses);
        }

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddCourse()
        {
            return View();
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> AddCourse(CourseDto dto)
        {
            try
            {
				var course = await _course.AddCourse(dto);
                return RedirectToAction("Index");
			}
			catch (Exception ex)
            {
                ViewBag.Error= ex.Message;
                return View();
            }

        }
        [Authorize(Roles ="Admin")]
        [HttpPost("/{id}")]

        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _course.DeleteCourse(id);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                ViewBag.Error= ex.Message;
                return View();
            }
        }

		[Authorize(Roles = "Admin")]
		[HttpGet("Course/UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id)
        {
             
            return View();
        }

		[Authorize(Roles = "Admin")]
		[HttpPost("Course/UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id,CourseDto dto)
        {
            try
            {
                await _course.UpdateCourse(id,dto);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                ViewBag.Error= ex.Message;
                return View();
            }
        }
    }
}
