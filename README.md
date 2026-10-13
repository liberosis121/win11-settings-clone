# HW1012 – Windows 11 Settings Home Clone

Bài tập môn Lập trình Windows: mô phỏng giao diện Windows 11 Settings Home bằng các control WinUI 3 và dữ liệu giả. Ứng dụng không thay đổi Windows Settings thật.

## Công nghệ và môi trường

- C#, XAML, WinUI 3, Windows App SDK `2.5.1`.
- Target framework: `net8.0-windows10.0.19041.0`.
- Windows SDK BuildTools `10.0.28000.2705`.
- Windows và Visual Studio 2026, có công cụ phát triển WinUI/C# và Windows SDK phù hợp. Máy hiện tại đã có Visual Studio Community 2026 và .NET SDK `10.0.401`.

Không thêm NuGet package, không đổi SDK/target framework; dùng XAML và code-behind với các control WinUI có sẵn.

## Mở, build và chạy

1. Trong Visual Studio 2026, chọn **Open a project or solution** và mở `SettingsClone.slnx`.
2. Chờ NuGet restore hoàn tất. Đặt `SettingsClone` làm startup project nếu cần.
3. Chọn cấu hình **Debug**, nền tảng **x64**, và launch profile **SettingsClone (Package)**.
4. Chọn **Build > Build Solution** (`Ctrl+Shift+B`). Nhấn `F5` để chạy và debug, hoặc `Ctrl+F5` để chạy không debug.

Có thể build bằng MSBuild của Visual Studio trong PowerShell:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SettingsClone.slnx /restore /t:Build /p:Configuration=Debug /p:Platform=x64 /verbosity:minimal /nologo
```

Đường dẫn MSBuild cần điều chỉnh nếu Visual Studio được cài ở vị trí khác. Nếu Visual Studio báo cần Developer Mode khi triển khai/debug gói MSIX, bật Developer Mode trong Windows rồi chạy lại.

Để đối chiếu ảnh đề, maximize cửa sổ và giữ display scaling phù hợp. Sử dụng profile **SettingsClone (Package)**; mở trực tiếp file `.exe` trong thư mục build có thể thiếu context gói MSIX và báo lỗi khởi tạo Windows App Runtime. Build bằng MSBuild không tự deploy ứng dụng như Visual Studio.

## Cấu trúc hiện tại

```text
SettingsClone/
├── SettingsClone.slnx                 # Solution
├── SettingsClone.csproj               # Project WinUI 3
├── App.xaml / App.xaml.cs             # Tài nguyên và khởi động ứng dụng
├── MainWindow.xaml / MainWindow.xaml.cs
├── Package.appxmanifest               # Cấu hình gói MSIX
├── app.manifest                       # Manifest ứng dụng Windows
├── Pages/
│   ├── HomePage.xaml / HomePage.xaml.cs
│   └── SystemPage.xaml / SystemPage.xaml.cs
├── Assets/                            # Asset template và bốn minh họa SVG nhỏ
│   └── ASSETS.md                      # Nguồn và giới hạn của minh họa
├── Properties/
│   ├── launchSettings.json            # Profile chạy
│   └── PublishProfiles/               # Profile win-x86, win-x64, win-arm64
├── References/
│   └── settings-reference.png         # Ảnh giao diện tham khảo
├── docs/
│   ├── HW1012_WinUI3_Lecture_Notes_for_Codex.md
│   ├── IMPLEMENTATION_VALIDATION.md    # Kết quả build/chạy và checklist review
│   └── screenshots/                   # Ảnh chụp ứng dụng thật ở các kích thước
├── .gitignore
├── AGENTS.md                          # Quy tắc cho coding agent, bằng tiếng Anh
└── README.md
```

`.vs/`, `bin/`, `obj/` và `SettingsClone.csproj.user` là dữ liệu cục bộ, không đưa vào Git.

## Trạng thái bài tập

Đã triển khai Home với thông tin thiết bị, Wi-Fi/Update và đủ bốn card: Recommended settings, Bluetooth devices, Try Microsoft 365, Cloud storage. Các hàng, divider, nút, toggle, progress và menu ba chấm dùng control thật.

`MainWindow` chứa thanh tiêu đề/tìm kiếm, sidebar 12 mục và Frame. Home là trang mặc định; Home/System điều hướng bằng `Frame.Navigate`. Back chỉ hoạt động khi `CanGoBack`; selected state cập nhật theo trang. System là trang phụ đơn giản với các hàng setting và nút Back to Home.

Hai cột card là hai StackPanel độc lập trong Grid. Khi nội dung Home hẹp hơn 760 effective pixels, chúng chuyển thành một cột theo thứ tự Recommended → Bluetooth → Microsoft 365 → Cloud storage. Sidebar thu gọn thành icon khi cửa sổ hẹp hơn 760 effective pixels; tooltip vẫn cho biết tên mục. Nội dung Home có một ScrollViewer; sidebar cuộn riêng khi thiếu chiều cao.

Màu và style dùng chung nằm trong `App.xaml`. Thanh tiêu đề sử dụng control TitleBar có sẵn, giữ nút Minimize/Maximize/Close thật của Windows. Tham khảo API: [Microsoft Learn – TitleBar](https://learn.microsoft.com/en-us/windows/apps/develop/ui/controls/title-bar).

## Tương tác mock

- Bluetooth mặc định Off và cả hai nút Connect disabled. Toggle chỉ thay đổi trạng thái demo, không điều khiển Bluetooth thật.
- Rename thay đổi tên hiển thị của thiết bị demo; không cho lưu tên trắng. Tên và trạng thái Bluetooth được giữ khi quay lại Home trong cùng phiên chạy.
- Nhập `Home` hoặc `System` vào ô tìm kiếm rồi nhấn Enter để mở trang tương ứng. Tìm kiếm khác hiển thị thông báo demo.
- Connect, các hàng Recommended settings, Add/View devices, menu thiết bị, Microsoft 365, storage/backup và các mục sidebar khác hiển thị thông báo mock. Không mở dịch vụ, thay đổi Windows hoặc giả vờ đăng ký/kết nối thành công.
- Các dữ liệu thiết bị, Wi-Fi, tài khoản, dung lượng và Windows Update đều là dữ liệu mẫu; email dùng địa chỉ `example.com`.

## Kiểm thử và giới hạn

Build **Debug/x64 thành công** trên Windows với Visual Studio MSBuild. Đã chạy ứng dụng dưới context gói MSIX, kiểm tra UI Automation cho navigation, Back, selected state, Bluetooth/Connect, Rename, tìm kiếm và dialog mock; chụp và xem UI ở ba kích thước.

Ảnh review: [Home lớn](docs/screenshots/home-large.png), [Home vừa](docs/screenshots/home-medium.png), [Home nhỏ](docs/screenshots/home-small.png), [Home nhỏ khi cuộn xuống](docs/screenshots/home-small-bottom.png), [System](docs/screenshots/system.png). Chi tiết nằm trong [kết quả kiểm thử](docs/IMPLEMENTATION_VALIDATION.md).

Các icon sidebar dùng FontIcon và hình nhỏ là SVG tự dựng, nên khác icon màu/3D và wallpaper gốc. Giao diện không nhúng screenshot làm nền. Bản hiện tại chưa được kiểm chứng trên x86/ARM64, các máy Windows khác, nhiều mức DPI, high contrast hoặc screen reader đầy đủ.

Trước khi nộp: review UI và các commit, build/chạy lại bằng Visual Studio, kiểm tra resize và mock actions. Clean Solution trước khi đóng gói bài; bỏ `.vs/`, `bin/`, `obj/` và file IDE cá nhân, giữ mã nguồn, solution/project, tài nguyên và tài liệu cần thiết để build lại.
