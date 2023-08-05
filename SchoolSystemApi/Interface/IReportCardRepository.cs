using SchoolSystemApi.Models;

namespace SchoolSystemApi;

public interface IReportCardRepository
{
    ICollection<ReportCard> GetReportCard();
    ReportCard GetReportCardById(int Id);
    bool reportCardExists(int Id);
    bool AddInReportCard(ReportCard reportCard);
    bool UpdateReportCard(ReportCard reportCard);
    bool DeleteReportCard(ReportCard reportCard);
    bool Save();
}