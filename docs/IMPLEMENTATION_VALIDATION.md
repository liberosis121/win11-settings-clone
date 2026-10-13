# HW1012 – Kết quả triển khai và kiểm thử

Ngày kiểm tra: **14/10/2026**, múi giờ Asia/Saigon. Kết quả thuộc bản đã bổ sung AdaptiveTrigger, truyền dữ liệu, DataTemplate, dialog dùng chung và icon màu.

## Môi trường và build

- Windows 11, Visual Studio Community 2026.
- .NET SDK `10.0.401`, target `net8.0-windows10.0.19041.0`.
- Giữ nguyên Windows App SDK `2.5.1` và SDK BuildTools `10.0.28000.2705`.
- Cấu hình **Debug/x64**: MSBuild kết thúc với **exit code 0**, không báo lỗi hoặc cảnh báo trong lượt build mới nhất.

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SettingsClone.slnx /restore /t:Build /p:Configuration=Debug /p:Platform=x64 /verbosity:minimal /nologo
```

Lần build đầu báo thiếu extension `GetAwaiter` khi await `ContentDialog.ShowAsync()` trong các Page; đã thêm `using System` và build lại thành công.

## Khởi chạy thực tế

Ứng dụng đã được chạy và quan sát, không chỉ kiểm tra mã nguồn. Để thử ngoài IDE, lấy file từ `.build.appxrecipe` của MSBuild vào layout `AppX`, đăng ký manifest của chính ứng dụng bằng `Add-AppxPackage -Register`, rồi kích hoạt AppUserModelID qua Windows ApplicationActivationManager.

Lệnh MSBuild `/t:Deploy` không có target tương ứng trong project này. Chạy `.exe` trực tiếp cũng từng báo `REGDB_E_CLASSNOTREG` khi khởi tạo runtime vì thiếu context gói. Các lượt chạy và kiểm thử thành công sử dụng cơ chế kích hoạt gói MSIX; trong Visual Studio hãy dùng **SettingsClone (Package)**.

Wallpaper Windows Bloom và các SVG mới có khai báo Content, có trong layout gói và đã hiển thị trong app. Bộ tài nguyên gồm 18 SVG và một JPG; 17 SVG dùng trong giao diện, DeviceWallpaper.svg là minh họa cũ không còn dùng. Nguồn/giấy phép: [ASSETS.md](../Assets/ASSETS.md).

## Những điểm đã cải thiện sau review

| Điểm review | Thay đổi thực tế | Cách xác minh |
| --- | --- | --- |
| Chưa truyền dữ liệu giữa Page | Home tạo DeviceInfo; Navigate truyền tham số; System nhận qua OnNavigatedTo | Rename thành HW1012-DEMO; xác nhận tên/model/Bluetooth On và Off trên System |
| Responsive chỉ có code-behind | VisualStateManager/AdaptiveTrigger và Setters trong XAML | Resize lớn → nhỏ → lớn; kiểm tra hai cột/một cột, sidebar 304/228/72 và nhóm nút |
| Lặp sidebar/hàng System | ItemsControl với một DataTemplate cho mỗi danh sách, Binding tới model đơn giản | Đủ 12 mục sidebar, sáu hàng System; selected state/tương tác đúng |
| Lặp dialog ở ba file | DialogHelper và một khóa chống mở chồng toàn cửa sổ | Connect, Rename, Microsoft 365, Apps và các luồng navigation |
| Spacing/tooltip cập nhật thừa | ColumnSpacing 14 khi có chữ, 0 khi thu gọn; tooltip Binding trong template; cột icon đủ 24 | Review XAML/C# và ảnh nhỏ; SizeChanged của shell chỉ tính chiều rộng ô tìm kiếm |
| NavigationFailed làm crash | e.Handled = true và dialog Page unavailable | Đã build/review nhánh xử lý; chưa xác minh được bằng gây lỗi ở runtime |
| Icon và wallpaper giản lược | Windows Bloom, sidebar màu, tám logo ứng dụng và OneDrive từ nguồn Microsoft; chỉnh Lock screen/Date & time/tai nghe | Xem ảnh chụp mới và đối chiếu ảnh cá nhân sinh viên |

Home giữ ngưỡng theo vùng nội dung thật: C# chuyển breakpoint nội dung/card sang MinWindowWidth; các thuộc tính layout do Setters XAML thay đổi. Page chứa shell và UserControl trong item template cung cấp control chủ quản cho state triggers. Kiểm tra chạy thật đã phát hiện và sửa trường hợp Grid trực tiếp dưới Window chưa tự kích hoạt các trạng thái.

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
- System nhận HW1012-DEMO, model và Bluetooth On cùng lúc qua DeviceInfo; cũng xác nhận giá trị Off. Back về Home giữ tên sau Rename; System/About dùng dữ liệu nhận từ Home.
- Ô tìm kiếm trên title bar nhận input bàn phím; Enter với System/Home điều hướng đúng.

Tên máy và trạng thái Bluetooth được trả lại giá trị mặc định sau kiểm tra. Không gọi API thay đổi Windows Settings, Bluetooth, cloud storage hoặc subscription.

Đã thử tạo bản sao tạm với Page cố ý lỗi để kiểm tra NavigationFailed, nhưng việc kích hoạt gói chưa chạy đúng bản thử nghiệm; không coi nhánh lỗi này là đã qua kiểm thử runtime. Page/hook thử nghiệm không nằm trong repository. Các luồng navigation bình thường được kiểm tra trên gói phát triển của bài làm.

## Responsive và kiểm tra trực quan

Display scaling của máy kiểm tra: **125% (120 DPI)**. Kích thước dưới đây là vùng client theo pixel vật lý, gồm title bar tùy biến; khác với effective pixels dùng trong XAML.

| Vùng client chụp thực tế | Layout quan sát |
| --- | --- |
| 1920 × 1140, maximize | Sidebar đầy đủ và hai cột card độc lập |
| 1422 × 951 | Hai cột co giãn; hàng thiết bị chuyển nút xuống dưới khi card hẹp |
| 882 × 851 | Sidebar dạng icon, card xếp một cột; cuộn tới được Cloud storage và PC backup |

Ngoài ảnh lưu, đã thử cửa sổ rộng 1100 pixel vật lý để kiểm tra sidebar mức vừa 228 effective pixels và client rộng 522 pixel để kiểm tra PC backup vẫn truy cập được. Sau khi maximize lại, hai cột khôi phục đúng. UI Automation còn kiểm tra chiều rộng mục Home ở chế độ icon để tránh bỏ sót sidebar chưa thu gọn.

Đã xem các screenshot và so sánh Home lớn với **ảnh cá nhân sinh viên**, không gọi ảnh này là ảnh gốc của giảng viên; xem [lưu ý nguồn ảnh trong README](../README.md). Các vùng chính, nội dung card, khoảng cách và hệ thống màu đã được chỉnh; vùng cuộn giới hạn theo viewport để căn lề cùng tiêu đề. Ảnh được chụp khi cửa sổ app nằm trên cùng, kiểm tra không bị ứng dụng khác che, rồi được xem trước khi đưa vào repository.

Kiểm tra screenshot cũng phát hiện highlight menu cũ còn tồn tại dù IsChecked đã chuyển Off. Template được sửa để dùng các trạng thái Checked/CheckedPointerOver/CheckedPressed cùng CommonStates theo [mã nguồn WinUI của Microsoft](https://github.com/microsoft/microsoft-ui-xaml/blob/main/controls/dev/CommonStyles/ToggleButton_themeresources.xaml). SystemPage cũng được giới hạn theo viewport để tránh lệch và cắt nội dung ngang.

Đã xác nhận qua vị trí UI Automation rằng hai tiêu đề card nằm ở các cột khác nhau tại kích thước vừa, cùng trục ngang sau khi cuộn tại kích thước nhỏ, và trạng thái PC backup nhìn thấy khi cuộn tới cuối. Không thấy chồng lấn rõ ràng trong các ảnh đã xem.

- [Home lớn](screenshots/home-large.png)
- [Home vừa](screenshots/home-medium.png)
- [Home nhỏ](screenshots/home-small.png)
- [Home nhỏ, phần dưới](screenshots/home-small-bottom.png)
- [System](screenshots/system.png)
- [System nhận tên mới và Bluetooth On](screenshots/system-parameter-demo.png)

## Khác biệt và phạm vi chưa xác minh

- Một số icon sidebar, Copilot/Designer và tai nghe vẫn dựng gần giống; không khẳng định giống từng pixel. Nguồn tài nguyên được ghi trong `Assets/ASSETS.md`.
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
5. Tài liệu bài giảng đã được theo dõi trong Git và giữ nguyên nội dung.
6. Clean Solution trước khi tạo gói bài nộp, loại dữ liệu IDE/build cục bộ và giữ đủ source/assets để build lại.
