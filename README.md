# HW1012 – Windows 11 Settings Home Clone

Bài tập môn Lập trình Windows: mô phỏng giao diện Windows 11 Settings Home bằng các control WinUI 3 và dữ liệu giả. Ứng dụng không thay đổi Windows Settings thật.

## Công nghệ và môi trường

- C#, XAML, WinUI 3, Windows App SDK `2.5.1`.
- Target framework: `net8.0-windows10.0.19041.0`.
- Windows SDK BuildTools `10.0.28000.2705`.
- Windows và Visual Studio 2026, có công cụ phát triển WinUI/C# và Windows SDK phù hợp. Máy hiện tại đã có Visual Studio Community 2026 và .NET SDK `10.0.401`.

## Mở, build và chạy

1. Trong Visual Studio 2026, chọn **Open a project or solution** và mở `SettingsClone.slnx`.
2. Chờ NuGet restore hoàn tất. Đặt `SettingsClone` làm startup project nếu cần.
3. Chọn cấu hình **Debug**, nền tảng **x64**, và launch profile **SettingsClone (Package)**.
4. Chọn **Build > Build Solution** (`Ctrl+Shift+B`). Nhấn `F5` để chạy và debug, hoặc `Ctrl+F5` để chạy không debug.

Có thể build bằng MSBuild của Visual Studio trong PowerShell:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SettingsClone.slnx /restore /t:Build /p:Configuration=Debug /p:Platform=x64 /verbosity:minimal /nologo
```

Đường dẫn MSBuild cần điều chỉnh nếu Visual Studio được cài ở vị trí khác. Nếu Visual Studio báo cần Developer Mode khi triển khai/debug gói MSIX, bật Developer Mode trong Windows rồi chạy lại. Giữ nguyên phiên bản SDK và NuGet hiện có khi thiết lập repository.

## Cấu trúc hiện tại

```text
SettingsClone/
├── SettingsClone.slnx                 # Solution
├── SettingsClone.csproj               # Project WinUI 3
├── App.xaml / App.xaml.cs             # Tài nguyên và khởi động ứng dụng
├── MainWindow.xaml / MainWindow.xaml.cs
├── Package.appxmanifest               # Cấu hình gói MSIX
├── app.manifest                       # Manifest ứng dụng Windows
├── Assets/                            # Logo và ảnh của ứng dụng
├── Properties/
│   ├── launchSettings.json            # Profile chạy
│   └── PublishProfiles/               # Profile win-x86, win-x64, win-arm64
├── References/
│   └── settings-reference.png         # Ảnh giao diện tham khảo
├── .gitignore
├── AGENTS.md                          # Quy tắc cho coding agent, bằng tiếng Anh
└── README.md
```

`.vs/`, `bin/`, `obj/` và `SettingsClone.csproj.user` là dữ liệu cục bộ, không đưa vào Git.

## Trạng thái bài tập

Hiện chỉ thiết lập repository và quy tắc làm việc. `MainWindow` còn là cửa sổ nền Mica với Grid trống; giao diện Settings Home chưa được triển khai. Chưa có HomePage hoặc SystemPage.

Bước tiếp theo: xem `References/settings-reference.png`, thống nhất bố cục và chia nhỏ việc dựng giao diện bằng Grid, StackPanel, Border và các control cơ bản; thêm Frame/Page khi cần điều hướng. Chỉ bắt đầu triển khai sau khi có yêu cầu.
