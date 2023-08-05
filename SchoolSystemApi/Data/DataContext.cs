using Microsoft.EntityFrameworkCore;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<ReportCard> ReportCard { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<CourseAndStudent> CoursesAndStudents { get; set; }
}