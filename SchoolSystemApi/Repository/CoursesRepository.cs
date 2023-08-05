using SchoolSystemApi.Data;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Repository;

public class CoursesRepository : ICoursesRepository
{
    private readonly DataContext _context;

    public CoursesRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Course> GetCourses()
    {
        return _context.Courses.ToList();
    }

    public Course GetCourse(int Id)
    {
        return _context.Courses.Where(c => c.Id == Id).FirstOrDefault();
    }

    public bool CourseExists(int Id)
    {
        return _context.Courses.Any(c => c.Id == Id);
    }

    public bool CreateCoures(Course course)
    {
        _context.Add(course);
        return Save();
    }

    public bool UpdateCourse(Course course)
    {
        _context.Update(course);
        return Save();
    }

    public bool DeleteCourse(Course course)
    {
        _context.Remove(course);
        return Save();
    }


    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}