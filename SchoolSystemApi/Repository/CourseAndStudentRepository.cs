using SchoolSystemApi.Data;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Repository;

public class CourseAndStudentRepository : ICourseAndStudentRepository
{
    private readonly DataContext _context;

    public CourseAndStudentRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<CourseAndStudent> GetCoursesAndStudents()
    {
        return _context.CoursesAndStudents.ToList();
    }

    public CourseAndStudent GetCourseAndStudent(int Id)
    {
        return _context.CoursesAndStudents.Where(c => c.Id == Id).FirstOrDefault();
    }

    public bool CourseAndStudentExists(int Id)
    {
        return _context.CoursesAndStudents.Any(cs => cs.Id == Id);
    }

    public bool CreateCourseAndStudents(CourseAndStudent courseAndStudent)
    {
        _context.Add(courseAndStudent);
        return Save();
    }

    public bool UpdateCourseAndStudents(CourseAndStudent courseAndStudent)
    {
        _context.Update(courseAndStudent);
        return Save();
    }

    public bool DeleteCourseAndStudents(CourseAndStudent courseAndStudent)
    {
        _context.Remove(courseAndStudent);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}