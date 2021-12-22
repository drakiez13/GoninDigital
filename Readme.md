# Gonin Digital

Ứng dụng mua bán thiết bị, linh kiện điện tử

## Sinh viên thực hiện

| STT | MSSV     | Họ và tên                | Lớp      |
| --- | -------- | ------------------------ | -------- |
| 0   | 19520951 | Trần Quốc Thắng          | KHTN2019 |
| 1   | 19521599 | Châu Ngọc Huy            | KHTN2019 |
| 2   | 19521855 | Trần Công Minh           | KHTN2019 |
| 3   | 19520166 | Phan Nhật Minh           | KHTN2019 |
| 4   | 19521287 | Nguyễn Văn Chính         | KHTN2019 |

## Hướng dẫn cài đặt
1. Cài đặt Visual Studio và SQL Server
2. Download hoặc clone reposity về máy
3. Tạo database dựa trên file Assets/database.sql
4. Thay đổi thông tin server lưu trữ database ở file Properties/Settings.settings
5. Mở project bằng Visual Studio và chạy file GoninDigital.sln (đảm bảo kết nối mạng)

## Công dụng
- Tạo ra môi trường trao đổi mua bán các thiết bị, linh kiện điện tử online. Gồm 3 loại tài khoản:
  + Quản Lý (Admin):
Là người quản lý người mua và cửa hàng, có khả năng thêm, xóa, sửa người dùng, cửa hàng và mặt hàng họ bán. Xét duyệt các mặt hàng trước khi cửa hàng đăng lên bán. Ngoài ra quản lý còn phải biết được tình trạng kinh doanh của các cửa hàng (doanh thu, doanh số, số đơn hàng, số mặt hàng,..). Đồng thời quản lý cần phải biết được các thông số của người mua (số đơn hàng đã đặt,...)
  + Cửa hàng (Vendor):
Là đối tác của tổ chức có thể bán hàng trên hệ thống. Có khả năng thêm, xóa, sửa mặt hàng của của mình. Có khả năng quản lý các đơn hàng đã được đặt, xem doanh số của cửa hàng và doanh số của các mặt hàng cụ thể. Đồng thời có thể xem được doanh thu của cửa hàng.
  + Người dùng (User):
Là người sử dụng hệ thống để mua hàng. Có khả năng xem thông tin, mua và yêu thích các mặt hàng trên hệ thống, xem thông tin của các cửa hàng. Có khả năng thêm, xóa và mua các mặt hàng trong giỏ hàng. 

