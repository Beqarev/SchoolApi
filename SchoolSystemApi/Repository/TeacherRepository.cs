using SchoolSystemApi.Data;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Repository;

public class TeacherRepository : ITeacherRepository
{
    private readonly DataContext _context;

    public TeacherRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Teacher> GetTeachers()
    {
        return _context.Teachers.ToList();
    }

    public Teacher GetTeacher(int Id)
    {
        return _context.Teachers.Where(t => t.Id == Id).FirstOrDefault();
    }

    public bool TeacherExists(int Id)
    {
        return _context.Teachers.Any(t => t.Id == Id);
    }

    public bool CreateTeacher(Teacher teacher)
    {
        _context.Add(teacher);
        return Save();
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        _context.Update(teacher);
        return Save();
    }

    public bool DeleteTeacher(Teacher teacher)
    {
        _context.Remove(teacher);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}