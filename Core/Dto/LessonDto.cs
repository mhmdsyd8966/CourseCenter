using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class LessonDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public string VideoLink { get; set; }
        public string Photo { get; set; }
    }
    public class UpdateLessonDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string Photo { get; set; }
    }
}
