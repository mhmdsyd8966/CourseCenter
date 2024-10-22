using Core;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.IRepository;
using Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CenterApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));

//DI
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt => { }).AddEntityFrameworkStores<CenterApplicationDbContext>()
    .AddDefaultTokenProviders().AddSignInManager<SignInManager<IdentityUser>>();

builder.Services.AddIdentityCore<Student>(opt => { }).AddRoles<IdentityRole>().AddEntityFrameworkStores<CenterApplicationDbContext>()
    .AddDefaultTokenProviders().AddSignInManager<SignInManager<Student>>();
builder.Services.AddIdentityCore<Teacher>(opt => { }).AddRoles<IdentityRole>().AddEntityFrameworkStores<CenterApplicationDbContext>()
    .AddDefaultTokenProviders().AddSignInManager<SignInManager<Teacher>>();
builder.Services.AddIdentityCore<Admin>(opt => { }).AddRoles<IdentityRole>().AddEntityFrameworkStores<CenterApplicationDbContext>()
    .AddDefaultTokenProviders().AddSignInManager<SignInManager<Admin>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
