using SchoolSystemApi.Data;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;

    public StudentRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Student> GetStudents()
    {
        return _context.Students.ToList();
    }

    public Student GetStudent(int Id)
    {
        return _context.Students.Where(s => s.Id == Id).FirstOrDefault();
    }

    public bool StudentExists(int Id)
    {
        return _context.Students.Any(s => s.Id == Id);
    }

    public bool CreateStudent(Student student)
    {
        _context.Students.Add(student);
        return Save();
    }

    public bool UpdateStudent(Student student)
    {
        _context.Update(student);
        return Save();
    }

    public bool DeleteStudent(Student student)
    {
        _context.Remove(student);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}