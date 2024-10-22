using Core.Dto;
using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseCenter.Models
{
	public class AddLessonDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string VideoLink { get; set; }
		public IFormFile Photo { get; set; }

		public LessonDto AddLessonDtoToLessonDto( AddLessonDto dto)
		{
			var lesson = new LessonDto
			{
				Description = dto.Description,
				Name = dto.Name,
				VideoLink = dto.VideoLink,
			};
			return lesson;
		}
		public UpdateLessonDto AddLessonDtoToUpdateLessonDto(AddLessonDto dto)
		{
			var lesson = new UpdateLessonDto
			{
				Description = dto.Description,
				Name = dto.Name,
				VideoLink = dto.VideoLink,

			};
			return lesson;
		}
	}
}
