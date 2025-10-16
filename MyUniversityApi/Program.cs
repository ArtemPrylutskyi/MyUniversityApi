using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


public class Student
{
    public int Id { get; set; } 
    [Required]
    public string FirstName { get; set; } = string.Empty; 
    [Required]
    public string LastName { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
}

public class Course
{
    public int Id { get; set; } 
    [Required]
    public string Title { get; set; } = string.Empty; 
    public int Credits { get; set; }
}


public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
}



[ApiController]
[Route("api/students")] 
public class StudentsController : ControllerBase
{
    
    private readonly UniversityDbContext _context;

    public StudentsController(UniversityDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound(); 
        }
        return student;
    }

    
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        student.EnrollmentDate = DateTime.UtcNow; 
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest(); 
        }

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }
}


[ApiController]
[Route("api/courses")] 
public class CoursesController : ControllerBase
{
    private readonly UniversityDbContext _context;

    public CoursesController(UniversityDbContext context)
    {
        _context = context;
    }

   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCourses), new { id = course.Id }, course);
    }
}


[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = "/")
    {
        
        return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = returnUrl }, "Google");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
      
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("You have been logged out.");
    }

    
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        if (User.Identity.IsAuthenticated)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { Message = "You are authenticated!", UserClaims = claims });
        }
        return Unauthorized("You are not authenticated.");
    }
}



public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddAuthentication(options =>
        {
           
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "Google";
        })
        .AddCookie() 
        .AddGoogle(options =>
        {
            
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        });

        
        builder.Services.AddAuthorization();
      

        builder.Services.AddControllers();
        builder.Services.AddDbContext<UniversityDbContext>(options =>
            options.UseInMemoryDatabase("UniversityDb"));

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

      
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
