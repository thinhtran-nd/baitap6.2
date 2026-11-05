namespace baitap6._2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private TabPage employeesTab;
        private TabPage attendanceTab;
        private TextBox txtSearch;
        private ComboBox cmbSearchDepartment;
        private Button btnSearch;
        private DataGridView dgvEmployees;
        private TextBox txtHoTen;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtCCCD;
        private ComboBox cmbPhongBan;
        private NumericUpDown numLuongCoBan;
        private NumericUpDown numHeSoLuong;
        private NumericUpDown numMaNV;
        private Button btnNew;
        private Button btnSaveEmployee;
        private Button btnDeleteEmployee;
        private DateTimePicker dtpThangNam;
        private Button btnLoadAttendance;
        private Button btnSaveAttendance;
        private Button btnClosePayroll;
        private DataGridView dgvAttendance;
        private Label lblEmployeeId;
        private Label lblHoTen;
        private Label lblNgaySinh;
        private Label lblCCCD;
        private Label lblPhongBan;
        private Label lblLuongCoBan;
        private Label lblHeSoLuong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl = new TabControl();
            employeesTab = new TabPage();
            dgvEmployees = new DataGridView();
            btnDeleteEmployee = new Button();
            btnSaveEmployee = new Button();
            btnNew = new Button();
            numHeSoLuong = new NumericUpDown();
            numLuongCoBan = new NumericUpDown();
            numMaNV = new NumericUpDown();
            cmbPhongBan = new ComboBox();
            txtCCCD = new TextBox();
            dtpNgaySinh = new DateTimePicker();
            txtHoTen = new TextBox();
            lblEmployeeId = new Label();
            lblHoTen = new Label();
            lblNgaySinh = new Label();
            lblCCCD = new Label();
            lblPhongBan = new Label();
            lblLuongCoBan = new Label();
            lblHeSoLuong = new Label();
            btnSearch = new Button();
            cmbSearchDepartment = new ComboBox();
            txtSearch = new TextBox();
            attendanceTab = new TabPage();
            dgvAttendance = new DataGridView();
            btnClosePayroll = new Button();
            btnSaveAttendance = new Button();
            btnLoadAttendance = new Button();
            dtpThangNam = new DateTimePicker();
            tabControl.SuspendLayout();
            employeesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLuongCoBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaNV).BeginInit();
            attendanceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(employeesTab);
            tabControl.Controls.Add(attendanceTab);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1084, 661);
            tabControl.TabIndex = 0;
            // 
            // employeesTab
            // 
            employeesTab.Controls.Add(dgvEmployees);
            employeesTab.Controls.Add(btnDeleteEmployee);
            employeesTab.Controls.Add(btnSaveEmployee);
            employeesTab.Controls.Add(btnNew);
            employeesTab.Controls.Add(numHeSoLuong);
            employeesTab.Controls.Add(numLuongCoBan);
            employeesTab.Controls.Add(numMaNV);
            employeesTab.Controls.Add(cmbPhongBan);
            employeesTab.Controls.Add(txtCCCD);
            employeesTab.Controls.Add(dtpNgaySinh);
            employeesTab.Controls.Add(txtHoTen);
            employeesTab.Controls.Add(lblEmployeeId);
            employeesTab.Controls.Add(lblHoTen);
            employeesTab.Controls.Add(lblNgaySinh);
            employeesTab.Controls.Add(lblCCCD);
            employeesTab.Controls.Add(lblPhongBan);
            employeesTab.Controls.Add(lblLuongCoBan);
            employeesTab.Controls.Add(lblHeSoLuong);
            employeesTab.Controls.Add(btnSearch);
            employeesTab.Controls.Add(cmbSearchDepartment);
            employeesTab.Controls.Add(txtSearch);
            employeesTab.Location = new Point(4, 29);
            employeesTab.Name = "employeesTab";
            employeesTab.Padding = new Padding(12);
            employeesTab.Size = new Size(1076, 628);
            employeesTab.TabIndex = 0;
            employeesTab.Text = "Nhân viên";
            employeesTab.UseVisualStyleBackColor = true;
            // 
            // dgvEmployees
            // 
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.AllowUserToDeleteRows = false;
            dgvEmployees.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmployees.ColumnHeadersHeight = 29;
            dgvEmployees.Location = new Point(16, 184);
            dgvEmployees.MultiSelect = false;
            dgvEmployees.Name = "dgvEmployees";
            dgvEmployees.ReadOnly = true;
            dgvEmployees.RowHeadersVisible = false;
            dgvEmployees.RowHeadersWidth = 51;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.Size = new Size(1044, 430);
            dgvEmployees.TabIndex = 13;
            dgvEmployees.SelectionChanged += dgvEmployees_SelectionChanged;
            // 
            // btnDeleteEmployee
            // 
            btnDeleteEmployee.Location = new Point(256, 138);
            btnDeleteEmployee.Name = "btnDeleteEmployee";
            btnDeleteEmployee.Size = new Size(100, 30);
            btnDeleteEmployee.TabIndex = 12;
            btnDeleteEmployee.Text = "Xóa";
            btnDeleteEmployee.UseVisualStyleBackColor = true;
            btnDeleteEmployee.Click += btnDeleteEmployee_Click;
            // 
            // btnSaveEmployee
            // 
            btnSaveEmployee.Location = new Point(126, 138);
            btnSaveEmployee.Name = "btnSaveEmployee";
            btnSaveEmployee.Size = new Size(120, 30);
            btnSaveEmployee.TabIndex = 11;
            btnSaveEmployee.Text = "Lưu nhân viên";
            btnSaveEmployee.UseVisualStyleBackColor = true;
            btnSaveEmployee.Click += btnSaveEmployee_Click;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(16, 138);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(100, 30);
            btnNew.TabIndex = 10;
            btnNew.Text = "Thêm mới";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // numHeSoLuong
            // 
            numHeSoLuong.DecimalPlaces = 2;
            numHeSoLuong.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numHeSoLuong.Location = new Point(886, 96);
            numHeSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numHeSoLuong.Name = "numHeSoLuong";
            numHeSoLuong.Size = new Size(120, 27);
            numHeSoLuong.TabIndex = 9;
            numHeSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numLuongCoBan
            // 
            numLuongCoBan.DecimalPlaces = 2;
            numLuongCoBan.Location = new Point(726, 96);
            numLuongCoBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numLuongCoBan.Name = "numLuongCoBan";
            numLuongCoBan.Size = new Size(150, 27);
            numLuongCoBan.TabIndex = 8;
            numLuongCoBan.ThousandsSeparator = true;
            // 
            // numMaNV
            // 
            numMaNV.Location = new Point(95, 54);
            numMaNV.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numMaNV.Name = "numMaNV";
            numMaNV.Size = new Size(150, 27);
            numMaNV.TabIndex = 20;
            // 
            // cmbPhongBan
            // 
            cmbPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPhongBan.Location = new Point(536, 96);
            cmbPhongBan.Name = "cmbPhongBan";
            cmbPhongBan.Size = new Size(180, 28);
            cmbPhongBan.TabIndex = 7;
            // 
            // txtCCCD
            // 
            txtCCCD.Location = new Point(366, 96);
            txtCCCD.Name = "txtCCCD";
            txtCCCD.PlaceholderText = "CCCD";
            txtCCCD.Size = new Size(160, 27);
            txtCCCD.TabIndex = 6;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(226, 96);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(130, 27);
            dtpNgaySinh.TabIndex = 5;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(16, 96);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.PlaceholderText = "Họ và tên";
            txtHoTen.Size = new Size(200, 27);
            txtHoTen.TabIndex = 4;
            // 
            // lblEmployeeId
            // 
            lblEmployeeId.AutoSize = true;
            lblEmployeeId.Location = new Point(16, 56);
            lblEmployeeId.Name = "lblEmployeeId";
            lblEmployeeId.Size = new Size(58, 20);
            lblEmployeeId.TabIndex = 3;
            lblEmployeeId.Text = "Mã NV ";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(16, 76);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(73, 20);
            lblHoTen.TabIndex = 14;
            lblHoTen.Text = "Họ và tên";
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(226, 76);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(74, 20);
            lblNgaySinh.TabIndex = 15;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // lblCCCD
            // 
            lblCCCD.AutoSize = true;
            lblCCCD.Location = new Point(366, 76);
            lblCCCD.Name = "lblCCCD";
            lblCCCD.Size = new Size(47, 20);
            lblCCCD.TabIndex = 16;
            lblCCCD.Text = "CCCD";
            // 
            // lblPhongBan
            // 
            lblPhongBan.AutoSize = true;
            lblPhongBan.Location = new Point(536, 76);
            lblPhongBan.Name = "lblPhongBan";
            lblPhongBan.Size = new Size(80, 20);
            lblPhongBan.TabIndex = 17;
            lblPhongBan.Text = "Phòng ban";
            // 
            // lblLuongCoBan
            // 
            lblLuongCoBan.AutoSize = true;
            lblLuongCoBan.Location = new Point(726, 76);
            lblLuongCoBan.Name = "lblLuongCoBan";
            lblLuongCoBan.Size = new Size(100, 20);
            lblLuongCoBan.TabIndex = 18;
            lblLuongCoBan.Text = "Lương cơ bản";
            // 
            // lblHeSoLuong
            // 
            lblHeSoLuong.AutoSize = true;
            lblHeSoLuong.Location = new Point(886, 76);
            lblHeSoLuong.Name = "lblHeSoLuong";
            lblHeSoLuong.Size = new Size(90, 20);
            lblHeSoLuong.TabIndex = 19;
            lblHeSoLuong.Text = "Hệ số lương";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(526, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 26);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cmbSearchDepartment
            // 
            cmbSearchDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSearchDepartment.Location = new Point(296, 16);
            cmbSearchDepartment.Name = "cmbSearchDepartment";
            cmbSearchDepartment.Size = new Size(220, 28);
            cmbSearchDepartment.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(16, 16);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm theo tên hoặc CCCD";
            txtSearch.Size = new Size(270, 27);
            txtSearch.TabIndex = 0;
            // 
            // attendanceTab
            // 
            attendanceTab.Controls.Add(dgvAttendance);
            attendanceTab.Controls.Add(btnClosePayroll);
            attendanceTab.Controls.Add(btnSaveAttendance);
            attendanceTab.Controls.Add(btnLoadAttendance);
            attendanceTab.Controls.Add(dtpThangNam);
            attendanceTab.Location = new Point(4, 29);
            attendanceTab.Name = "attendanceTab";
            attendanceTab.Padding = new Padding(12);
            attendanceTab.Size = new Size(1076, 628);
            attendanceTab.TabIndex = 1;
            attendanceTab.Text = "Chấm công & lương";
            attendanceTab.UseVisualStyleBackColor = true;
            // 
            // dgvAttendance
            // 
            dgvAttendance.AllowUserToAddRows = false;
            dgvAttendance.AllowUserToDeleteRows = false;
            dgvAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAttendance.ColumnHeadersHeight = 29;
            dgvAttendance.Location = new Point(16, 62);
            dgvAttendance.MultiSelect = false;
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.RowHeadersVisible = false;
            dgvAttendance.RowHeadersWidth = 51;
            dgvAttendance.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvAttendance.Size = new Size(1044, 552);
            dgvAttendance.TabIndex = 4;
            // 
            // btnClosePayroll
            // 
            btnClosePayroll.Location = new Point(436, 16);
            btnClosePayroll.Name = "btnClosePayroll";
            btnClosePayroll.Size = new Size(150, 28);
            btnClosePayroll.TabIndex = 3;
            btnClosePayroll.Text = "Chốt lương tháng";
            btnClosePayroll.UseVisualStyleBackColor = true;
            btnClosePayroll.Click += btnClosePayroll_Click;
            // 
            // btnSaveAttendance
            // 
            btnSaveAttendance.Location = new Point(296, 16);
            btnSaveAttendance.Name = "btnSaveAttendance";
            btnSaveAttendance.Size = new Size(130, 28);
            btnSaveAttendance.TabIndex = 2;
            btnSaveAttendance.Text = "Lưu chấm công";
            btnSaveAttendance.UseVisualStyleBackColor = true;
            btnSaveAttendance.Click += btnSaveAttendance_Click;
            // 
            // btnLoadAttendance
            // 
            btnLoadAttendance.Location = new Point(156, 16);
            btnLoadAttendance.Name = "btnLoadAttendance";
            btnLoadAttendance.Size = new Size(130, 28);
            btnLoadAttendance.TabIndex = 1;
            btnLoadAttendance.Text = "Tải chấm công";
            btnLoadAttendance.UseVisualStyleBackColor = true;
            btnLoadAttendance.Click += btnLoadAttendance_Click;
            // 
            // dtpThangNam
            // 
            dtpThangNam.CustomFormat = "MM/yyyy";
            dtpThangNam.Format = DateTimePickerFormat.Custom;
            dtpThangNam.Location = new Point(16, 18);
            dtpThangNam.Name = "dtpThangNam";
            dtpThangNam.ShowUpDown = true;
            dtpThangNam.Size = new Size(130, 27);
            dtpThangNam.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 661);
            Controls.Add(tabControl);
            MinimumSize = new Size(900, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HRM & Payroll System";
            Load += Form1_Load;
            tabControl.ResumeLayout(false);
            employeesTab.ResumeLayout(false);
            employeesTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLuongCoBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaNV).EndInit();
            attendanceTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
