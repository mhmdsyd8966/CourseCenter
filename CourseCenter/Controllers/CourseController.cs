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
                return Json(new
                {
                    Success=true,
                    Message="Course Added Successfully"
                });
			}
			catch (Exception ex)
            {
                return Json(new
                {
                    Sucess = false,
                    message = ex.Message
                });
            }

        }
        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _course.DeleteCourse(id);

				return Json(new
				{
					Success = true,
					Message = "Course Deleted Successfully"
				});
			}
			catch(Exception ex)
            {
				return Json(new
				{
					Sucess = false,
					message = ex.Message
				});
			}
        }

		[Authorize(Roles = "Admin")]
		[HttpGet("Course/UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id)
        {
             
            return View();
        }

		[Authorize(Roles = "Admin")]
		[HttpPut("Course/UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id,CourseDto dto)
        {
            try
            {
                await _course.UpdateCourse(id,dto);
                return Json(new
                {
                    Success = true,
                    Message = "Course Update Successfully"
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    Sucess = false,
                    message = ex.Message
                });
            }
        }
    }
}
