using System.Globalization;
using System.Data;
using baitap6._2.Data;

namespace baitap6._2.Business;

internal sealed class PayrollService
{
    private readonly HrRepository _repository;

    public PayrollService(HrRepository repository)
    {
        _repository = repository;
    }

    public DataTable LoadAttendance(string month) => _repository.GetAttendance(ValidateMonth(month));

    public void SaveAttendance(DataTable table, string month) =>
        _repository.SaveAttendance(table, ValidateMonth(month));

    public int ClosePayroll(string month) => _repository.ClosePayroll(ValidateMonth(month));

    private static string ValidateMonth(string month)
    {
        if (!DateTime.TryParseExact(month, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            throw new InvalidOperationException("Select a valid month and year.");
        }

        return month;
    }
}
