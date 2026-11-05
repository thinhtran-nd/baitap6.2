using System.Data;
using Microsoft.Data.SqlClient;
using baitap6._2.Models;

namespace baitap6._2.Data;

internal sealed class HrRepository
{
    public List<Department> GetDepartments()
    {
        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        using var command = new SqlCommand("SELECT MaPB, TenPB FROM dbo.PhongBan ORDER BY TenPB", connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        var departments = new List<Department>();
        while (reader.Read())
        {
            departments.Add(new Department
            {
                MaPB = reader.GetInt32(0),
                TenPB = reader.GetString(1)
            });
        }

        return departments;
    }

    public List<Employee> GetEmployees(string searchText, int? departmentId)
    {
        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        using var command = new SqlCommand(
            "SELECT MaNV, HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong " +
            "FROM dbo.NhanVien " +
            "WHERE (@Search = N'' OR HoTen LIKE @NameSearch OR CCCD = @Search) " +
            "AND (@MaPB IS NULL OR MaPB = @MaPB) ORDER BY HoTen", connection);
        command.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = searchText.Trim();
        command.Parameters.Add("@NameSearch", SqlDbType.NVarChar, 202).Value = $"%{searchText.Trim()}%";
        command.Parameters.Add("@MaPB", SqlDbType.Int).Value = departmentId.HasValue ? departmentId.Value : DBNull.Value;

        connection.Open();
        using var reader = command.ExecuteReader();
        var employees = new List<Employee>();
        while (reader.Read())
        {
            employees.Add(new Employee
            {
                MaNV = reader.GetInt32(0),
                HoTen = reader.GetString(1),
                NgaySinh = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                CCCD = reader.IsDBNull(3) ? null : reader.GetString(3),
                MaPB = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                LuongCoBan = reader.GetDecimal(5),
                HeSoLuong = reader.GetDouble(6)
            });
        }

        return employees;
    }

    public void SaveEmployee(Employee employee, bool isNewEmployee)
    {
        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        connection.Open();

        if (isNewEmployee && employee.MaNV > 0)
        {
            using var transaction = connection.BeginTransaction();
            using var explicitIdCommand = new SqlCommand(
                "SET IDENTITY_INSERT dbo.NhanVien ON; " +
                "BEGIN TRY " +
                "INSERT INTO dbo.NhanVien (MaNV, HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) " +
                "VALUES (@MaNV, @HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong); " +
                "SET IDENTITY_INSERT dbo.NhanVien OFF; " +
                "END TRY " +
                "BEGIN CATCH " +
                "SET IDENTITY_INSERT dbo.NhanVien OFF; " +
                "THROW; " +
                "END CATCH;", connection, transaction);
            AddEmployeeParameters(explicitIdCommand, employee);
            explicitIdCommand.Parameters.Add("@MaNV", SqlDbType.Int).Value = employee.MaNV;

            try
            {
                explicitIdCommand.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            return;
        }

        using var command = new SqlCommand(isNewEmployee
            ? "INSERT INTO dbo.NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) " +
              "VALUES (@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong)"
            : "UPDATE dbo.NhanVien SET HoTen = @HoTen, NgaySinh = @NgaySinh, CCCD = @CCCD, " +
              "MaPB = @MaPB, LuongCoBan = @LuongCoBan, HeSoLuong = @HeSoLuong WHERE MaNV = @MaNV", connection);

        AddEmployeeParameters(command, employee);
        if (!isNewEmployee)
        {
            command.Parameters.Add("@MaNV", SqlDbType.Int).Value = employee.MaNV;
        }

        var affected = command.ExecuteNonQuery();
        if (!isNewEmployee && affected == 0)
        {
            throw new InvalidOperationException("The employee record no longer exists.");
        }
    }

    public void DeleteEmployee(int employeeId)
    {
        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        using var command = new SqlCommand("DELETE FROM dbo.NhanVien WHERE MaNV = @MaNV", connection);
        command.Parameters.Add("@MaNV", SqlDbType.Int).Value = employeeId;
        connection.Open();
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("The employee record no longer exists.");
        }
    }

    public DataTable GetAttendance(string month)
    {
        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        using var adapter = new SqlDataAdapter(
            "SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot " +
            "FROM dbo.BangChamCong WHERE ThangNam = @ThangNam", connection);
        adapter.SelectCommand!.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

        var table = new DataTable("BangChamCong");
        adapter.Fill(table);

        var employees = new Dictionary<int, string>();
        using (var command = new SqlCommand("SELECT MaNV, HoTen FROM dbo.NhanVien", connection))
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(reader.GetInt32(0), reader.GetString(1));
            }
        }

        table.Columns.Add("HoTen", typeof(string));
        foreach (DataRow row in table.Rows)
        {
            row["HoTen"] = employees[Convert.ToInt32(row["MaNV"])];
        }

        table.Columns["MaCong"]!.AutoIncrement = true;
        table.Columns["MaCong"]!.AutoIncrementSeed = -1;
        table.Columns["MaCong"]!.AutoIncrementStep = -1;

        var existingEmployeeIds = table.AsEnumerable()
            .Select(row => row.Field<int>("MaNV"))
            .ToHashSet();
        foreach (var employee in employees)
        {
            if (existingEmployeeIds.Contains(employee.Key))
            {
                continue;
            }

            var row = table.NewRow();
            row["MaNV"] = employee.Key;
            row["ThangNam"] = month;
            row["SoNgayCong"] = 0d;
            row["SoGioOT"] = 0d;
            row["DaChot"] = false;
            row["HoTen"] = employee.Value;
            table.Rows.Add(row);
        }

        return table;
    }

    public void SaveAttendance(DataTable table, string month)
    {
        if (table.AsEnumerable().Any(row => row.RowState != DataRowState.Deleted && row.Field<bool>("DaChot")))
        {
            throw new InvalidOperationException("Attendance for this month has already been closed.");
        }

        foreach (DataRow row in table.Rows)
        {
            if (row.RowState == DataRowState.Deleted)
            {
                continue;
            }

            var workDays = Convert.ToDouble(row["SoNgayCong"]);
            var overtimeHours = Convert.ToDouble(row["SoGioOT"]);
            if (workDays is < 0 or > 31 || overtimeHours < 0)
            {
                throw new InvalidOperationException("Work days must be between 0 and 31, and overtime cannot be negative.");
            }

            row["ThangNam"] = month;
        }

        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        using var adapter = new SqlDataAdapter(
            "SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot " +
            "FROM dbo.BangChamCong WHERE ThangNam = @ThangNam", connection);
        adapter.SelectCommand!.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        using var builder = new SqlCommandBuilder(adapter);
        adapter.Update(table);
    }

    public int ClosePayroll(string month)
    {
        using var connection = new SqlConnection(DatabaseSettings.ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            using var command = new SqlCommand(
                "IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien) " +
                "    THROW 51001, 'There are no employees to process.', 1; " +
                "IF EXISTS (SELECT 1 FROM dbo.BangLuong WHERE ThangNam = @ThangNam) " +
                "    THROW 51002, 'Payroll has already been closed for this month.', 1; " +
                "IF EXISTS (SELECT 1 FROM dbo.NhanVien nv " +
                "    LEFT JOIN dbo.BangChamCong cc ON cc.MaNV = nv.MaNV AND cc.ThangNam = @ThangNam " +
                "    WHERE cc.MaCong IS NULL) " +
                "    THROW 51003, 'Save attendance for every employee before closing payroll.', 1; " +
                "INSERT INTO dbo.BangLuong (MaNV, ThangNam, ThucLinh) " +
                "SELECT nv.MaNV, @ThangNam, CAST(ROUND((nv.LuongCoBan * nv.HeSoLuong / 22.0) * cc.SoNgayCong " +
                "+ (cc.SoGioOT * 50000.0), 2) AS decimal(18,2)) " +
                "FROM dbo.NhanVien nv " +
                "INNER JOIN dbo.BangChamCong cc ON cc.MaNV = nv.MaNV AND cc.ThangNam = @ThangNam; " +
                "UPDATE dbo.BangChamCong SET DaChot = 1 WHERE ThangNam = @ThangNam AND DaChot = 0; " +
                "SELECT @@ROWCOUNT;", connection, transaction);
            command.Parameters.Add("@ThangNam", SqlDbType.VarChar, 7).Value = month;
            var closedRows = Convert.ToInt32(command.ExecuteScalar());
            transaction.Commit();
            return closedRows;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static void AddEmployeeParameters(SqlCommand command, Employee employee)
    {
        command.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = employee.HoTen.Trim();
        command.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = employee.NgaySinh.HasValue ? employee.NgaySinh.Value : DBNull.Value;
        command.Parameters.Add("@CCCD", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(employee.CCCD) ? DBNull.Value : employee.CCCD.Trim();
        command.Parameters.Add("@MaPB", SqlDbType.Int).Value = employee.MaPB.HasValue ? employee.MaPB.Value : DBNull.Value;
        command.Parameters.Add("@LuongCoBan", SqlDbType.Decimal).Value = employee.LuongCoBan;
        command.Parameters["@LuongCoBan"].Precision = 18;
        command.Parameters["@LuongCoBan"].Scale = 2;
        command.Parameters.Add("@HeSoLuong", SqlDbType.Float).Value = employee.HeSoLuong;
    }
}
