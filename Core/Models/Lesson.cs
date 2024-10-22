using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string VideoLink { get; set; }
        public string Photo { get; set; }
    }
}
