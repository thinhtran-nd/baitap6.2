using System.Data;
using System.Globalization;
using baitap6._2.Business;
using baitap6._2.Data;
using baitap6._2.Models;

namespace baitap6._2;

public partial class Form1 : Form
{
    private readonly EmployeeService _employeeService;
    private readonly PayrollService _payrollService;
    private int? _selectedEmployeeId;
    private DataTable? _attendanceTable;
    private string? _attendanceMonth;

    public Form1()
    {
        InitializeComponent();
        var repository = new HrRepository();
        _employeeService = new EmployeeService(repository);
        _payrollService = new PayrollService(repository);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        RunSafely(() =>
        {
            LoadDepartments();
            RefreshEmployees();
        });
    }

    private void btnSearch_Click(object? sender, EventArgs e) => RunSafely(RefreshEmployees);

    private void btnNew_Click(object? sender, EventArgs e) => ClearEmployeeForm();

    private void btnSaveEmployee_Click(object? sender, EventArgs e)
    {
        RunSafely(() =>
        {
            var isNewEmployee = !_selectedEmployeeId.HasValue;
            _employeeService.SaveEmployee(new Employee
            {
                MaNV = isNewEmployee ? (int)numMaNV.Value : _selectedEmployeeId!.Value,
                HoTen = txtHoTen.Text,
                NgaySinh = dtpNgaySinh.Value.Date,
                CCCD = txtCCCD.Text,
                MaPB = cmbPhongBan.SelectedValue is int departmentId && departmentId > 0 ? departmentId : null,
                LuongCoBan = numLuongCoBan.Value,
                HeSoLuong = (double)numHeSoLuong.Value
            }, isNewEmployee);

            RefreshEmployees();
            if (isNewEmployee)
            {
                ClearEmployeeForm();
            }

            MessageBox.Show(this, "Đã lưu hồ sơ nhân viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
    }

    private void btnDeleteEmployee_Click(object? sender, EventArgs e)
    {
        if (!_selectedEmployeeId.HasValue)
        {
            MessageBox.Show(this, "Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show(this, "Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        RunSafely(() =>
        {
            _employeeService.DeleteEmployee(_selectedEmployeeId.Value);
            ClearEmployeeForm();
            RefreshEmployees();
        });
    }

    private void dgvEmployees_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvEmployees.SelectedRows.Count == 0 || dgvEmployees.SelectedRows[0].DataBoundItem is not Employee employee)
        {
            return;
        }

        _selectedEmployeeId = employee.MaNV;
        lblEmployeeId.Text = "Mã NV";
        numMaNV.Value = employee.MaNV;
        numMaNV.Enabled = false;
        txtHoTen.Text = employee.HoTen;
        dtpNgaySinh.Value = employee.NgaySinh ?? DateTime.Today;
        txtCCCD.Text = employee.CCCD ?? string.Empty;
        cmbPhongBan.SelectedValue = employee.MaPB ?? 0;
        numLuongCoBan.Value = Math.Min(numLuongCoBan.Maximum, employee.LuongCoBan);
        numHeSoLuong.Value = Math.Min(numHeSoLuong.Maximum, Math.Max(numHeSoLuong.Minimum, (decimal)employee.HeSoLuong));
    }

    private void btnLoadAttendance_Click(object? sender, EventArgs e)
    {
        if (!ConfirmAttendanceDiscard())
        {
            return;
        }

        RunSafely(() => LoadAttendanceGrid(GetSelectedMonth()));
    }

    private void btnSaveAttendance_Click(object? sender, EventArgs e)
    {
        RunSafely(() =>
        {
            if (_attendanceTable is null || _attendanceMonth is null)
            {
                throw new InvalidOperationException("Vui lòng tải bảng chấm công trước.");
            }

            if (_attendanceMonth != GetSelectedMonth())
            {
                throw new InvalidOperationException("Vui lòng tải lại bảng chấm công cho tháng đã chọn trước khi lưu.");
            }

            CommitAttendanceEdit();
            _payrollService.SaveAttendance(_attendanceTable, _attendanceMonth);
            LoadAttendanceGrid(_attendanceMonth);
            MessageBox.Show(this, "Đã lưu bảng chấm công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
    }

    private void btnClosePayroll_Click(object? sender, EventArgs e)
    {
        RunSafely(() =>
        {
            var month = GetSelectedMonth();
            if (_attendanceTable?.GetChanges() is not null && _attendanceMonth != month)
            {
                throw new InvalidOperationException($"Bảng chấm công tháng {_attendanceMonth} chưa lưu. Hãy lưu hoặc bỏ thay đổi trước khi chốt tháng khác.");
            }

            if (MessageBox.Show(this, $"Chốt lương tháng {month}? Dữ liệu chấm công sẽ bị khóa.", "Xác nhận chốt lương", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            if (_attendanceTable is not null && _attendanceMonth == month)
            {
                CommitAttendanceEdit();
                _payrollService.SaveAttendance(_attendanceTable, month);
            }

            var closedRows = _payrollService.ClosePayroll(month);
            LoadAttendanceGrid(month);
            MessageBox.Show(this, $"Đã chốt lương cho {closedRows} nhân viên trong tháng {month}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
    }

    private void LoadDepartments()
    {
        var departments = _employeeService.GetDepartments();
        cmbPhongBan.DataSource = new List<DepartmentOption>
        {
            new(0, "Chưa phân công")
        }.Concat(departments.Select(department => new DepartmentOption(department.MaPB, department.TenPB))).ToList();
        cmbPhongBan.DisplayMember = nameof(DepartmentOption.Name);
        cmbPhongBan.ValueMember = nameof(DepartmentOption.Id);

        cmbSearchDepartment.DataSource = new List<DepartmentOption>
        {
            new(0, "Tất cả phòng ban")
        }.Concat(departments.Select(department => new DepartmentOption(department.MaPB, department.TenPB))).ToList();
        cmbSearchDepartment.DisplayMember = nameof(DepartmentOption.Name);
        cmbSearchDepartment.ValueMember = nameof(DepartmentOption.Id);
        cmbSearchDepartment.SelectedIndex = 0;
    }

    private void RefreshEmployees()
    {
        var departmentId = cmbSearchDepartment.SelectedValue is int id && id > 0 ? id : (int?)null;
        dgvEmployees.DataSource = _employeeService.SearchEmployees(txtSearch.Text, departmentId);
        SetEmployeeColumnHeaders();
        if (dgvEmployees.Rows.Count == 0)
        {
            ClearEmployeeForm();
        }
    }

    private void SetEmployeeColumnHeaders()
    {
        SetColumnHeader("MaNV", "Mã NV");
        SetColumnHeader("HoTen", "Họ và tên");
        SetColumnHeader("NgaySinh", "Ngày sinh");
        SetColumnHeader("CCCD", "CCCD");
        SetColumnHeader("MaPB", "Mã phòng ban");
        SetColumnHeader("LuongCoBan", "Lương cơ bản");
        SetColumnHeader("HeSoLuong", "Hệ số lương");
    }

    private void SetColumnHeader(string columnName, string headerText)
    {
        if (dgvEmployees.Columns.Contains(columnName))
        {
            dgvEmployees.Columns[columnName]!.HeaderText = headerText;
        }
    }

    private void ClearEmployeeForm()
    {
        _selectedEmployeeId = null;
        lblEmployeeId.Text = "Mã NV";
        numMaNV.Value = 0;
        numMaNV.Enabled = true;
        txtHoTen.Clear();
        dtpNgaySinh.Value = DateTime.Today;
        txtCCCD.Clear();
        cmbPhongBan.SelectedIndex = cmbPhongBan.Items.Count > 0 ? 0 : -1;
        numLuongCoBan.Value = 0;
        numHeSoLuong.Value = 1;
        dgvEmployees.ClearSelection();
    }

    private void LoadAttendanceGrid(string month)
    {
        _attendanceTable = _payrollService.LoadAttendance(month);
        _attendanceMonth = month;
        dgvAttendance.DataSource = _attendanceTable;
        SetAttendanceColumnHeaders();

        var isClosed = _attendanceTable.AsEnumerable().Any(row => row.Field<bool>("DaChot"));
        dgvAttendance.ReadOnly = isClosed;
        foreach (var columnName in new[] { "MaCong", "MaNV", "ThangNam", "DaChot", "HoTen" })
        {
            if (dgvAttendance.Columns.Contains(columnName))
            {
                dgvAttendance.Columns[columnName]!.ReadOnly = true;
            }
        }

        btnSaveAttendance.Enabled = !isClosed;
        btnClosePayroll.Enabled = !isClosed;
    }

    private void SetAttendanceColumnHeaders()
    {
        SetAttendanceColumnHeader("MaCong", "Mã công");
        SetAttendanceColumnHeader("MaNV", "Mã NV");
        SetAttendanceColumnHeader("HoTen", "Họ và tên");
        SetAttendanceColumnHeader("ThangNam", "Tháng/năm");
        SetAttendanceColumnHeader("SoNgayCong", "Số ngày công");
        SetAttendanceColumnHeader("SoGioOT", "Số giờ OT");
        SetAttendanceColumnHeader("DaChot", "Đã chốt");
        if (dgvAttendance.Columns.Contains("DaChot"))
        {
            dgvAttendance.Columns["DaChot"]!.Visible = false;
        }
    }

    private void SetAttendanceColumnHeader(string columnName, string headerText)
    {
        if (dgvAttendance.Columns.Contains(columnName))
        {
            dgvAttendance.Columns[columnName]!.HeaderText = headerText;
        }
    }

    private string GetSelectedMonth() => dtpThangNam.Value.ToString("MM/yyyy", CultureInfo.InvariantCulture);

    private void CommitAttendanceEdit()
    {
        dgvAttendance.EndEdit();
        if (_attendanceTable is not null)
        {
            BindingContext[_attendanceTable]?.EndCurrentEdit();
        }
    }

    private bool ConfirmAttendanceDiscard()
    {
        CommitAttendanceEdit();
        if (_attendanceTable?.GetChanges() is null)
        {
            return true;
        }

        return MessageBox.Show(this, "Bảng chấm công có thay đổi chưa lưu. Bạn có muốn bỏ các thay đổi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }

    private void RunSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private sealed record DepartmentOption(int Id, string Name);
}
