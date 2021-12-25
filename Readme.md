  # Gonin Digital
  
 ## 1. Giới thiệu
- Đồ án cuối kì môn Lập trình trực quan - IT008
- Tên đề tài: Phần nềm mua bán thiết bị, linh kiện điện tử online
- Giảng viên hướng dẫn: Thầy Nguyễn Tấn Toàn

## 2. Sinh viên thực hiện

| STT | MSSV     | Họ và tên                | Lớp      |
| --- | -------- | ------------------------ | -------- |
| 0   | 19520951 | Trần Quốc Thắng          | KHTN2019 |
| 1   | 19521599 | Châu Ngọc Huy            | KHTN2019 |
| 2   | 19521855 | Trần Công Minh           | KHTN2019 |
| 3   | 19520166 | Phan Nhật Minh           | KHTN2019 |
| 4   | 19521287 | Nguyễn Văn Chính         | KHTN2019 |


## 3. Mô tả
- Phần mềm giúp tạo ra môi trường trao đổi, mua bán các thiết bị, linh kiện điện tử online mà không cần phải đến tận nơi để lựa chọn và đánh giá. Người mua có thể theo dõi các sản phẩm trong mỗi cửa hàng cũng như tham khảo chất lượng đánh giá về sản phẩm, từ đó có thể đưa ra quyết định mua dễ dàng hơn. Đây cũng là nơi để các cửa hàng quảng bá sản phẩm của mình, giúp tiếp cận đến nhiều khách hàng hơn và nâng cao khả năng phát triển hơn. 
- Hệ thống gồm 3 loại tài khoản:
  + **Quản Lý (Admin):**
Là người quản lý người mua và cửa hàng, có khả năng thêm, xóa, sửa người dùng, cửa hàng và mặt hàng họ bán. Xét duyệt các mặt hàng trước khi cửa hàng đăng lên bán. Ngoài ra quản lý còn phải biết được tình trạng kinh doanh của các cửa hàng (doanh thu, doanh số, số đơn hàng, số mặt hàng,..). Đồng thời quản lý cần phải biết được các thông số của người mua (số đơn hàng đã đặt,...)
  + **Cửa hàng (Vendor):**
Là đối tác của tổ chức có thể bán hàng trên hệ thống. Có khả năng thêm, xóa, sửa mặt hàng của của mình. Có khả năng quản lý các đơn hàng đã được đặt, xem doanh số của cửa hàng và doanh số của các mặt hàng cụ thể. Đồng thời có thể xem được doanh thu của cửa hàng.
  + **Người dùng (User):**
Là người sử dụng hệ thống để mua hàng. Có khả năng xem thông tin, mua và yêu thích các mặt hàng trên hệ thống, xem thông tin của các cửa hàng. Có khả năng thêm, xóa và mua các mặt hàng trong giỏ hàng. 
## 4. Công nghệ sử dụng:
- Nền tảng: .NET core 5.0
- Frontend: XAML, C#, WPF
- Cơ sở dữ liệu: SQL Server
- IDE: Microsoft Visual Studio 2019
- Thư viện hỗ trợ: MordernWpfUI, Microsoft.Xaml.Behaviors.Wpf, Microsoft.EntityFrameworkCore, System.Drawing.Common
## 5. Hướng dẫn cài đặt
<details>
  <summary>Build from source </summary>
  
- Cài đặt Visual Studio và SQL Server
- Download hoặc clone reposity về máy
- Tạo database dựa trên file Assets/database.sql
- Mở project bằng Visual Studio và chạy file GoninDigital.sln 
 </details>
 <details>
  <summary>Install from package </summary>
  
- Download file zip tại mục release
- Giải nén file zip và mở để sử dụng 
 </details>



