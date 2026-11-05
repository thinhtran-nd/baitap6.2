using baitap6._2.Data;
using baitap6._2.Models;

namespace baitap6._2.Business;

internal sealed class EmployeeService
{
    private readonly HrRepository _repository;

    public EmployeeService(HrRepository repository)
    {
        _repository = repository;
    }

    public List<Department> GetDepartments() => _repository.GetDepartments();

    public List<Employee> SearchEmployees(string searchText, int? departmentId) =>
        _repository.GetEmployees(searchText, departmentId);

    public void SaveEmployee(Employee employee, bool isNewEmployee)
    {
        employee.HoTen = employee.HoTen.Trim();
        employee.CCCD = string.IsNullOrWhiteSpace(employee.CCCD) ? null : employee.CCCD.Trim();

        if (isNewEmployee && employee.MaNV < 0)
        {
            throw new InvalidOperationException("Employee ID cannot be negative.");
        }

        if (!isNewEmployee && employee.MaNV <= 0)
        {
            throw new InvalidOperationException("Select an employee to update.");
        }

        if (employee.HoTen.Length == 0)
        {
            throw new InvalidOperationException("Employee name is required.");
        }

        if (employee.HoTen.Length > 100)
        {
            throw new InvalidOperationException("Employee name cannot exceed 100 characters.");
        }

        if (employee.CCCD?.Length > 20)
        {
            throw new InvalidOperationException("Citizen ID cannot exceed 20 characters.");
        }

        if (employee.LuongCoBan <= 0)
        {
            throw new InvalidOperationException("Base salary must be greater than zero.");
        }

        if (employee.HeSoLuong <= 0)
        {
            throw new InvalidOperationException("Salary coefficient must be greater than zero.");
        }

        _repository.SaveEmployee(employee, isNewEmployee);
    }

    public void DeleteEmployee(int employeeId)
    {
        if (employeeId <= 0)
        {
            throw new InvalidOperationException("Select an employee to delete.");
        }

        _repository.DeleteEmployee(employeeId);
    }
}
