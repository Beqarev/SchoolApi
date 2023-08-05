using SchoolSystemApi.Data;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Repository;

public class ReportCardRepository : IReportCardRepository
{
    private readonly DataContext _context;

    public ReportCardRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<ReportCard> GetReportCard()
    {
        return _context.ReportCard.ToList();
    }

    public ReportCard GetReportCardById(int Id)
    {
        return _context.ReportCard.Where(r => r.Id == Id).FirstOrDefault();
    }

    public bool reportCardExists(int Id)
    {
        return _context.ReportCard.Any(r => r.Id == Id);
    }

    public bool AddInReportCard(ReportCard reportCard)
    {
        _context.Add(reportCard);
        return Save();
    }

    public bool UpdateReportCard(ReportCard reportCard)
    {
        _context.Update(reportCard);
        return Save();
    }

    public bool DeleteReportCard(ReportCard reportCard)
    {
        _context.Remove(reportCard);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}