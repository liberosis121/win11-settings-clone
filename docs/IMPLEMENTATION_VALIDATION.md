# HW1012 – Kết quả triển khai và kiểm thử

Ngày kiểm tra: **14/10/2026**, múi giờ Asia/Saigon.

## Môi trường và build

- Windows 11, Visual Studio Community 2026.
- .NET SDK `10.0.401`, target `net8.0-windows10.0.19041.0`.
- Giữ nguyên Windows App SDK `2.5.1` và SDK BuildTools `10.0.28000.2705`.
- Cấu hình **Debug/x64**: MSBuild kết thúc với **exit code 0**.

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SettingsClone.slnx /restore /t:Build /p:Configuration=Debug /p:Platform=x64 /verbosity:minimal /nologo
```

Lần build đầu báo thiếu extension `GetAwaiter` khi await `ContentDialog.ShowAsync()` trong các Page; đã thêm `using System` và build lại thành công.

## Khởi chạy thực tế

Ứng dụng đã được chạy và quan sát, không chỉ kiểm tra mã nguồn. Để thử ngoài IDE, lấy file từ `.build.appxrecipe` của MSBuild vào layout `AppX`, đăng ký manifest của chính ứng dụng bằng `Add-AppxPackage -Register`, rồi kích hoạt AppUserModelID qua Windows ApplicationActivationManager.

Lệnh MSBuild `/t:Deploy` không có target tương ứng trong project này. Chạy `.exe` trực tiếp cũng từng báo `REGDB_E_CLASSNOTREG` khi khởi tạo runtime vì thiếu context gói. Các lượt chạy và kiểm thử thành công sử dụng cơ chế kích hoạt gói MSIX; trong Visual Studio hãy dùng **SettingsClone (Package)**.

Bốn SVG có khai báo `Content` trong project, có mặt trong appxrecipe/layout và đã hiển thị trong ảnh chụp thực tế.

## Kiểm tra tương tác bằng UI Automation

Các kiểm tra sau đã đạt trên bản chạy thực tế:

- Home hiển thị mặc định; Back disabled khi chưa có lịch sử.
- Chọn lại Home không tạo lịch sử trùng hoặc làm mất selected state.
- Chọn System mở SystemPage, chuyển selected state từ Home sang System.
- Nút Back trên title bar quay về Home và disabled khi hết lịch sử.
- Nút Back to Home của SystemPage hoạt động.
- Bluetooth Off khóa cả hai nút Connect.
- Bluetooth On mở khóa cả hai nút; chuyển Off khóa lại.
- Connect hiển thị dialog mang tên demo, không thay đổi kết nối thật.
- Quay về Home giữ trạng thái Bluetooth demo nhờ cache Page.
- Try for free hiển thị dialog Microsoft 365 demo.
- Mục Apps chưa triển khai hiển thị dialog demo và vẫn giữ Home selected.
- Rename không cho lưu tên chỉ có khoảng trắng; tên hợp lệ cập nhật label thiết bị.
- Ô tìm kiếm trên title bar nhận input bàn phím; Enter với System/Home điều hướng đúng.

Tên máy và trạng thái Bluetooth được trả lại giá trị mặc định sau kiểm tra. Không gọi API thay đổi Windows Settings, Bluetooth, cloud storage hoặc subscription.

## Responsive và kiểm tra trực quan

Display scaling của máy kiểm tra: **125% (120 DPI)**. Kích thước dưới đây là vùng client theo pixel vật lý, gồm title bar tùy biến; khác với effective pixels dùng trong XAML.

| Vùng client chụp thực tế | Layout quan sát |
| --- | --- |
| 1920 × 1140, maximize | Sidebar đầy đủ và hai cột card độc lập |
| 1422 × 951 | Hai cột co giãn; hàng thiết bị chuyển nút xuống dưới khi card hẹp |
| 882 × 851 | Sidebar dạng icon, card xếp một cột; cuộn tới được Cloud storage và PC backup |

Đã xem các screenshot và so sánh Home lớn với ảnh đề. Các vùng chính, nội dung card, khoảng cách và hệ thống màu đã được chỉnh; sửa lệch ngang giữa tiêu đề Home với vùng ScrollViewer bằng cách giới hạn content theo viewport trước khi căn giữa.

Kiểm tra screenshot cũng phát hiện highlight menu cũ còn tồn tại dù IsChecked đã chuyển Off. Template được sửa để dùng các trạng thái Checked/CheckedPointerOver/CheckedPressed cùng CommonStates theo [mã nguồn WinUI của Microsoft](https://github.com/microsoft/microsoft-ui-xaml/blob/main/controls/dev/CommonStyles/ToggleButton_themeresources.xaml). SystemPage cũng được giới hạn theo viewport để tránh lệch và cắt nội dung ngang.

Đã xác nhận qua vị trí UI Automation rằng hai tiêu đề card nằm ở các cột khác nhau tại kích thước vừa, cùng trục ngang sau khi cuộn tại kích thước nhỏ, và trạng thái PC backup nhìn thấy khi cuộn tới cuối. Không thấy chồng lấn rõ ràng trong các ảnh đã xem.

- [Home lớn](screenshots/home-large.png)
- [Home vừa](screenshots/home-medium.png)
- [Home nhỏ](screenshots/home-small.png)
- [Home nhỏ, phần dưới](screenshots/home-small-bottom.png)
- [System](screenshots/system.png)

## Khác biệt và phạm vi chưa xác minh

- Hình thiết bị, logo cloud và hàng icon ứng dụng là SVG giản lược; sidebar dùng FontIcon một màu, khác asset gốc trong ảnh đề. Xem `Assets/ASSETS.md`.
- Email mẫu dùng `example.com`; dữ liệu Wi-Fi, thiết bị, dung lượng và update là mock.
- Screenshot không phải giao diện được nhúng; các card, chữ, nút và thanh tiến độ là control thật.
- Chỉ Home và System có Page; các mục khác thông báo demo theo phạm vi được duyệt.
- Chưa kiểm tra x86/ARM64, Windows phiên bản khác, nhiều màn hình/scaling khác, high contrast hoặc toàn bộ luồng screen reader.
- Kiểm tra dialog tự động tập trung vào các luồng chính; không khẳng định mọi mục trong menu flyout đã được click riêng lẻ.

## Checklist review và nộp bài

1. Mở `SettingsClone.slnx`, chọn Debug/x64 và SettingsClone (Package), build và chạy.
2. Maximize để đối chiếu ảnh; thử resize lớn/vừa/nhỏ và cuộn tới Add device/PC backup.
3. Thử Home/System/Back; thử tìm kiếm, Rename và Bluetooth demo.
4. Review lịch sử commit và `git diff`; không có thay đổi remote hoặc push trong nhiệm vụ này.
5. Tài liệu bài giảng đã có sẵn trước triển khai vẫn được giữ nguyên; người dùng quyết định việc track file này.
6. Clean Solution trước khi tạo gói bài nộp, loại dữ liệu IDE/build cục bộ và giữ đủ source/assets để build lại.
