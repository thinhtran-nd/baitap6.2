# baitap6.2
# BÁO CÁO BÀI TẬP / ĐỒ ÁN

## THÔNG TIN SINH VIÊN
- **Họ và tên:** Trần Xuân Thịnh
- **Mã số sinh viên:** 24810320263
- **Lớp:** D19QTANM1
- **Tên môn học:** Lập trình C# / Windows Forms
- **Tên bài tập:** BÀI 6.2: HỆ THỐNG QUẢN LÝ NHÂN SỰ & TÍNH LƯƠNG (HRM & PAYROLL SYSTEM)

---

## KẾT QUẢ THỰC HÀNH

- Giao diện chính
<img width="1550" height="935" alt="GD" src="https://github.com/user-attachments/assets/95771be6-6e2e-4118-9ad2-03ad4fbe1c38" />


- Xác nhận thời gian chấm công + OT của nhân viên
<img width="1549" height="931" alt="CN1" src="https://github.com/user-attachments/assets/82f4064c-78f1-484e-8ddf-fdd7a9aa91b8" />


- Tính tổng lương nhân viên
<img width="1553" height="931" alt="CN2" src="https://github.com/user-attachments/assets/470b1e97-b97b-43c9-b23e-3420d5b8c99d" />


## HƯỚNG DẪN CÀI ĐẶT VÀ CHẠY DỰ ÁN:
1. **Khởi tạo CSDL**:
   - Mở file `Database.sql` có sẵn trong thư mục dự án bằng SQL Server Management Studio (SSMS) hoặc Visual Studio.
   - Chạy toàn bộ script (Execute) để tự động tạo CSDL `HRMDB` cùng các bảng và dữ liệu mẫu.

2. **Cấu hình chuỗi kết nối**:
   - Mở file `Dbhelper.cs`.
   - Kiểm tra và đổi lại `Data Source` cho khớp với tên SQL Server trên máy chấm (Mặc định đang để kết nối tới `(localdb)\MSSQLLocalDB`).

3. **Chạy chương trình**:
   - Nhấn **F5** (hoặc nút Start) trên Visual Studio để chạy hệ thống.
