using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{    public class CenterApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public CenterApplicationDbContext(DbContextOptions<CenterApplicationDbContext> options):base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Exams> Exams { get; set; } 
        public DbSet<Question> Questions { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData([
                        new IdentityRole
                        {
                            Id = "a332353e-b918-4e0a-8446-09135972ed8b",
                            Name = "Student",
                            NormalizedName = "STUDENT",
                            ConcurrencyStamp = null
                        },
                    new IdentityRole
                    {
                        Id = "a8cc6873-26a4-4ab7-9ccf-0db3de4c4471",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = null
                    },
                    new IdentityRole
                    {
                        Id = "63614cdd-fb33-472e-8c3f-83d1a75b377f",
                        Name = "Teacher",
                        NormalizedName = "TEACHER",
                        ConcurrencyStamp = null
                    },
                ]);
            builder.Entity<Teacher>().ToTable(nameof(Teacher));
            builder.Entity<Student>().ToTable(nameof(Student));
            builder.Entity<Admin>().ToTable(nameof(Admin));
            builder.Entity<Exams>().HasOne(t=>t.Teacher).WithMany(x=>x.Exams).HasForeignKey(t=>t.TeacherId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Lesson>().HasOne(t => t.Teacher).WithMany().HasForeignKey(t => t.TeacherId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Exams>().HasMany(t => t.Students).WithMany(x => x.Exams);
            

            builder.Entity<Course>().HasData([
                new Course
                {
                    Id=1,
                    Name="Arabic",
                    NumberOfStudents=0,
                    NumberofTeachers=0,
                
                },
                new Course
                {
                    Id = 2,
                    Name = "Physics",
                    NumberOfStudents = 0,
                    NumberofTeachers = 0,

                },
                new Course
                {
                    Id = 3,
                    Name = "Anatomy",
                    NumberOfStudents = 0,
                    NumberofTeachers = 0,

                },
                new Course
                {
                    Id = 4,
                    Name = "Math",
                    NumberOfStudents = 0,
                    NumberofTeachers = 0,

                },
                new Course
                {
                    Id = 5,
                    Name = "English",
                    NumberOfStudents = 0,
                    NumberofTeachers = 0,

                },
            ]);
            
        }
    }
}
