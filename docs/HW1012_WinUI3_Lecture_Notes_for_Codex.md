# HW1012 — Tóm tắt bài giảng WinUI 3 dành cho Codex

> **Vai trò tài liệu:** Đây là ghi chú tổng hợp có chọn lọc từ slide môn *Lập trình Windows*, nhằm giúp coding agent thực hiện bài **HW1012 – Bắt chước màn hình Settings**. Tài liệu **không phải đề bài chính thức**, không thay thế ảnh bài tập, và không có nghĩa mọi tính năng đề cập bên dưới đều bắt buộc.
>
> **Ảnh chuẩn để đối chiếu:** `References/settings-reference.png` trong repository của sinh viên (kiểm tra file thực sự tồn tại trước khi phân tích). Nếu chưa có ảnh, yêu cầu người dùng bổ sung; không suy đoán chi tiết pixel hay tuyên bố đã xem ảnh.
>
> **Phạm vi công nghệ:** C#, XAML, **WinUI 3 / Windows App SDK** trong Visual Studio; không dùng WPF, UWP, MAUI, Uno hoặc web frontend.

## 1. Nguồn và mức độ ưu tiên

Tài liệu này tổng hợp từ các slide được cung cấp trong cuộc trò chuyện:

| Slide | Nội dung liên quan trực tiếp |
| --- | --- |
| `WP04 WinUI01 - Getting started.pptx` | WinUI 3, cấu trúc project, `MainWindow.xaml`/code-behind, `App.xaml.cs`, build/chạy, clean trước khi nộp, thư viện mẫu và lưu ý UI |
| `WP05 WinUI02 - Basic controls.pptx` | `TextBlock`, `Image`, `Button`, `SymbolIcon`, `TextBox`, `ContentDialog`, một số input controls; bài tập clone các màn hình Settings và gợi ý `SettingsCard` |
| `WP06 WinUI03 - Layout.pptx` | Responsive layout, kích thước hiệu dụng, spacing bội số 4, `StackPanel`, `Grid`, `Auto`/`*`, `Border`, `VisualStateManager`/`AdaptiveTrigger`, bài tập **Settings Home** ở maximum/medium/small size |
| `WP07 WinUI04 - Navigation.pptx` | Phân biệt `Window` với `Page`, `Frame.Navigate`, `GoBack`, `GoForward`, truyền dữ liệu qua tham số điều hướng và `OnNavigatedTo`, `NavigationView` |
| `WP15 AI driven development Github Copilot.pptx` | Cách dùng AI để sinh code, giải thích, sửa lỗi, tối ưu, kiểm thử; giới hạn context và cần kiểm tra mã do AI tạo |

**Ưu tiên cho HW1012:** Bám ảnh LMS và thực hành WP05–WP06 trước; WP07 chỉ dùng cho navigation nếu có thời gian hoặc được yêu cầu. WP15 hướng dẫn cách cộng tác với AI, không phải đặc tả kỹ thuật cho ứng dụng.

## 2. Những gì bài giảng thực sự nêu về bài Settings

- WP05 liệt kê các bài tập **Screen cloning**: `Settings Home`, `Settings System`, `Bluetooth & devices`, `Network`, `Personalization`.
- WP06 có **Homework – Settings Home**, cùng hình tham chiếu ở các mức **Maximum size** và **Medium size & Small size**.
- Vì vậy, tái hiện giao diện **Settings Home** là trọng tâm gần nhất với bài được giao. Khả năng co giãn kích thước cửa sổ đáng được cân nhắc, nhưng **chưa có rubric chấm điểm chi tiết trong các tài liệu được đọc**.
- Không có căn cứ từ các slide này để khẳng định phải xây dựng tất cả Settings pages, phải kết nối Settings hệ điều hành, phải có backend, hoặc phải dùng MVVM.

## 3. Nền tảng WinUI 3 và cấu trúc project (WP04)

Slide WP04 mô tả WinUI là lớp giao diện hiện đại trên Windows sử dụng **XAML**, đi cùng **Windows App SDK** và phong cách Fluent Design.

Các file quan trọng trong ứng dụng WinUI 3 cơ bản:

- `App.xaml.cs`: điểm khởi chạy ứng dụng (ví dụ `OnLaunched`), tạo/hiển thị cửa sổ.
- `MainWindow.xaml`: khai báo cấu trúc và các control bằng XAML.
- `MainWindow.xaml.cs`: code-behind, xử lý sự kiện cho giao diện.
- `.csproj`: cấu hình target framework, Windows App SDK, package và assets.
- `Assets/`: hình ảnh/tài nguyên đi kèm; đường dẫn và Build Action phải phù hợp.

Trong Visual Studio, slide WP04 hướng dẫn **Build** với `Ctrl + Shift + B` rồi chạy **không debug** với `Ctrl + F5`. Với bài này cần **build và chạy thực tế**, không chỉ nhìn code.

**Trước khi nộp:** slide WP04 nhắc **Clean solution**, loại `.vs`, `bin`, `obj` khỏi gói bài nộp; giữ solution/project, mã nguồn và tài nguyên cần thiết để build lại.

## 4. Basic controls (WP05)

Các control có ví dụ trong slide:

| Control | Công dụng theo bài giảng | Ứng dụng có thể có trong Settings Home |
| --- | --- | --- |
| `TextBlock` | Hiển thị chữ, có style, `Run`, `TextTrimming` | Heading, label, mô tả, trạng thái |
| `Image` | Hiển thị hình từ `Assets` hoặc nguồn ảnh | Avatar, hình minh họa |
| `Button` | Nút và sự kiện `Click` | Các hành động hoặc mở trang |
| `SymbolIcon` / `FontIcon` | Biểu tượng trong giao diện | Icon cho menu và card |
| `TextBox` | Nhập text, có `Header`, `Text` | Ô tìm setting dạng mô phỏng |
| `CheckBox`, `RadioButton`, `Slider`, `ToggleButton` | Điều khiển đầu vào/trạng thái | Chỉ dùng nếu có phần chức năng mock phù hợp |
| `ContentDialog` | Hộp thoại, cần gắn `XamlRoot` khi hiển thị từ page/window | Thông báo đơn giản, **không bắt buộc** cho ảnh clone |

Slide WP05 gợi ý `SettingsCard` từ **Community Toolkit** cho các màn Settings. Đây là một **phương án**, không phải bắt buộc. Nếu muốn bổ sung package, cần giải thích lý do và so sánh với cách dùng `Border`/`StackPanel` đơn giản.

Ví dụ XAML trong slide WP05 (rút gọn theo ý tưởng):

```xml
<StackPanel Orientation="Vertical"
            HorizontalAlignment="Center"
            VerticalAlignment="Center">
    <TextBlock Text="Profile" Style="{StaticResource TitleTextBlockStyle}" />
    <Image Source="Assets/gamer.png" Width="100" Height="100" />
    <TextBox x:Name="nameTextBox" Header="Enter your name:" />
    <Button Content="Salute" Click="saluteButton_Click" />
</StackPanel>
```

**Nguyên tắc học tập:** Ưu tiên control WinUI chuẩn và các sự kiện đơn giản có thể giải thích được. Không cần mô phỏng API hệ điều hành để có hiệu ứng giao diện.

## 5. Layout: StackPanel, Grid, Border (WP06)

### 5.1. StackPanel

`StackPanel` sắp xếp control theo một chiều. `Orientation="Vertical"` là danh sách dọc; `Orientation="Horizontal"` là một hàng ngang. Bài giảng nhấn mạnh `Orientation`, căn lề, `Padding` và các thuộc tính liên quan tới panel.

```xml
<StackPanel Orientation="Vertical" Spacing="12" Padding="16">
    <TextBlock Text="Home" />
    <Button Content="System" />
</StackPanel>
```

Phù hợp cho nhóm mục ở sidebar, nhóm nhãn/hình/nút bên trong card, hoặc stack nhiều card.

### 5.2. Grid

`Grid` dùng hàng/cột và attached properties như `Grid.Row`, `Grid.Column` để đặt phần tử. Trong WP06:

- `Auto`: kích thước dựa trên nội dung cần chứa.
- `*`: tỷ lệ phân phối **phần không gian còn lại** (ví dụ `2*` lớn hơn `*`).
- Width/Height bằng số: kích thước cố định; dùng có chủ đích và kiểm tra khi resize.

Ví dụ cho **một phương án thực hiện bài**, không phải code mẫu nguyên văn từ slide:

```xml
<Grid ColumnSpacing="16">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="260" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>

    <StackPanel Grid.Column="0" Spacing="8">
        <TextBlock Text="Settings" />
        <Button Content="Home" />
        <Button Content="System" />
    </StackPanel>

    <Frame x:Name="ContentFrame" Grid.Column="1" />
</Grid>
```

Sử dụng `Grid` cho layout lớn; dùng `StackPanel` cho nhóm phần tử theo hàng hoặc cột. **Không dựng toàn bộ màn hình bằng `Canvas` và tọa độ tuyệt đối** nếu muốn thích ứng với kích thước cửa sổ.

### 5.3. Border và spacing

WP06 bàn về border properties (`BorderBrush`, `BorderThickness`, `CornerRadius`, `Padding`) và cách căn giữa child bên trong `Border`. Tinh thần thiết kế trong slide: **kích thước, khoảng cách và margin thường là bội số của 4 effective pixels**.

Khi clone các cards, có thể dùng `Border` kết hợp `CornerRadius`, `Background`, `Padding`, chứa `StackPanel` hoặc `Grid`. Đây là gợi ý áp dụng, không phải yêu cầu mọi card đều là `Border`.

### 5.4. Scroll và responsive

Slide WP06 phân tích nhiều cách đáp ứng màn hình: reposition, resize, reflow, show/hide, replace và re-architect. Nó có ví dụ `VisualStateManager` + `AdaptiveTrigger` để thay đổi thuộc tính layout theo độ rộng cửa sổ, chẳng hạn đổi hướng `StackPanel`.

```xml
<Grid>
    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup>
            <VisualState>
                <VisualState.StateTriggers>
                    <AdaptiveTrigger MinWindowWidth="400" />
                </VisualState.StateTriggers>
                <VisualState.Setters>
                    <Setter Target="myPanel.Orientation" Value="Horizontal" />
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>

    <StackPanel x:Name="myPanel" Orientation="Vertical">
        <Rectangle Width="200" Height="100" Fill="Red" />
        <Rectangle Width="200" Height="100" Fill="Green" />
    </StackPanel>
</Grid>
```

Đây là ví dụ trong tinh thần slide, không phải thiết kế responsive hoàn chỉnh cho bài HW1012. `ScrollViewer` nên được cân nhắc cho nội dung dài. Khi thay đổi kích thước cửa sổ, nội dung cần không bị cắt, đè hoặc chạy ra ngoài khó sử dụng.

## 6. Navigation và truyền dữ liệu (WP07)

Slide phân biệt điều hướng giữa **Window** và điều hướng nhiều **Page** trong cùng **Frame**:

- `Frame.Navigate(...)`: chuyển sang `Page` khác.
- `Frame.GoBack()`, `Frame.GoForward()`: duyệt lịch sử điều hướng; kiểm tra điều kiện hỗ trợ (`CanGoBack`, `CanGoForward`) trước khi gọi.
- Có `BackStack`, `ForwardStack`, `BackStackDepth`.
- `NavigationView` là control tổ chức menu/navigation, có ví dụ lấy page từ `Tag` và điều hướng bằng `Frame`.

Ví dụ logic căn bản (minh họa đề xuất áp dụng, không phải chép mã slide):

```csharp
ContentFrame.Navigate(typeof(HomePage));
ContentFrame.Navigate(typeof(SystemPage));

// Truyền dữ liệu sang một Page:
ContentFrame.Navigate(typeof(SystemPage), "Hello from Home");
```

Bên trang đích:

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    base.OnNavigatedTo(e);
    if (e.Parameter is string message)
    {
        // Xử lý message.
    }
}
```

Cần `using` đúng namespace (ví dụ `Microsoft.UI.Xaml.Navigation`) khi triển khai thật.

Slide WP07 cũng nêu vấn đề **truyền object** và gợi ý `ICloneable` để xử lý tình huống chỉnh sửa dữ liệu mà không gây thay đổi ngoài ý muốn. **Không cần dùng `ICloneable` chỉ để chuyển giữa Home và System khi không có dữ liệu cần edit.**

Đối với HW1012, navigation có thể là phần mở rộng: dùng sidebar để đổi giữa `HomePage` và một trang phụ tối giản nếu muốn minh họa kỹ thuật vừa học; không mặc định phải clone toàn bộ các trang Settings.

## 7. Cách dùng AI hỗ trợ lập trình (WP15)

Slide WP15 nói về cách mô tả yêu cầu để AI tạo code và các tác vụ hỗ trợ như:

- Giải thích mã nguồn, tài liệu hóa.
- Cải thiện/refactor code.
- Tạo unit test ở nơi phù hợp.
- Gợi ý sửa lỗi.
- Chú ý context cuộc trò chuyện và những hạn chế của AI-assisted coding.

**Quy trình đề xuất để vận dụng cho HW1012** (đây là tổng hợp thực hành, không phải nguyên văn quy trình của WP15):

1. Đọc `AGENTS.md`, project, ảnh tham khảo, ghi chú này.
2. **Lập kế hoạch trước, chưa sửa code**; xin người dùng duyệt.
3. Chia nhỏ: khung `MainWindow`/sidebar → nội dung `HomePage` → polish → navigation tùy chọn → responsive/test.
4. Mỗi lần chỉ làm một giai đoạn; báo cáo file thay đổi và mục đích.
5. Chạy build nếu môi trường hỗ trợ; **không tuyên bố build thành công nếu chưa chạy**.
6. Người dùng chạy ứng dụng trên Windows bằng Visual Studio, kiểm tra giao diện, lỗi runtime và screenshot.
7. So sánh với ảnh gốc; điều chỉnh kích thước, màu sắc, spacing, icon; sau đó commit theo yêu cầu.

## 8. Đề xuất kiến trúc tối giản cho HW1012 (suy luận áp dụng, không phải yêu cầu slide)

**Chỉ triển khai khi người dùng duyệt.** Cấu trúc khả dĩ:

```text
RepositoryRoot/
├── AGENTS.md
├── README.md
├── References/
│   └── settings-reference.png
├── SettingsClone.slnx          # hoặc .sln; tên thực tế có thể khác
└── SettingsClone/
    ├── SettingsClone.csproj
    ├── App.xaml
    ├── App.xaml.cs
    ├── MainWindow.xaml
    ├── MainWindow.xaml.cs
    ├── Pages/
    │   ├── HomePage.xaml
    │   ├── HomePage.xaml.cs
    │   ├── SystemPage.xaml       # chỉ khi làm navigation
    │   └── SystemPage.xaml.cs    # chỉ khi làm navigation
    └── Assets/
```

Dự kiến bố cục:

```text
MainWindow
└── Grid (sidebar + nội dung)
    ├── Sidebar: StackPanel / các nút, nhãn, icon
    └── Frame → HomePage
         └── ScrollViewer
              └── StackPanel hoặc Grid
                   ├── Header / thiết bị
                   └── Khu card bố cục Grid + StackPanel
```

**Đừng sao chép cấu trúc này máy móc**: hãy kiểm tra repository và đối chiếu ảnh thật. Có thể dùng `NavigationView` nếu giúp bám sát UI hơn và người dùng đồng ý; `Frame + sidebar` thủ công thường dễ học hơn cho bài cơ bản.

### Ưu tiên thực hiện

**Thiết yếu cho bản clone:**
- Khởi chạy được ứng dụng WinUI 3.
- Có sidebar, tiêu đề và bố cục card đúng ảnh.
- Dùng XAML controls thật, không gắn nguyên screenshot làm background.
- Chữ, vị trí, padding, độ bo góc, biểu tượng, hình ảnh gần ảnh tham chiếu.
- Cửa sổ có thể resize mà không hỏng giao diện nghiêm trọng.

**Tùy chọn / mở rộng:**
- Click sidebar và navigation sang `SystemPage` mẫu.
- Search mock, Bluetooth toggle mock, feedback đơn giản.
- Responsive nâng cao nhiều breakpoint, `SettingsCard`/UserControl.

**Chưa có cơ sở bắt buộc từ đề và slide:** database, Supabase, API, đăng nhập thật, đọc/cập nhật thiết lập hệ thống, MVVM phức tạp, sao chép mọi trang Settings.

## 9. Checklist nghiệm thu với Codex

- [ ] Đã đọc `AGENTS.md` và ảnh tham khảo thực sự tồn tại.
- [ ] Đã xác định đúng project WinUI 3 / Windows App SDK, không đổi framework.
- [ ] Đã trình kế hoạch và được người dùng duyệt trước khi sửa.
- [ ] Mỗi bước thay đổi ít file, có giải thích.
- [ ] Build thành công trên Windows hoặc ghi rõ chưa kiểm chứng.
- [ ] Kiểm tra bố cục tại kích thước cửa sổ lớn và nhỏ.
- [ ] So sánh screenshot ứng dụng với ảnh yêu cầu.
- [ ] Không thêm backend, DB, package ngoài phạm vi khi chưa được duyệt.
- [ ] Không commit/push thay người dùng khi chưa được yêu cầu.
- [ ] Có thể giải thích được `Grid`, `StackPanel`, `Frame`, `Page`, `Navigate`, `OnNavigatedTo` nếu đã dùng.

## 10. Prompt mở đầu khuyến nghị

> Đọc `AGENTS.md`, `docs/HW1012_WinUI3_Lecture_Notes_for_Codex.md` và `References/settings-reference.png` (báo nếu thiếu file). Kiểm tra project WinUI 3 hiện tại. Dựa trên ảnh và kiến thức trong tài liệu, hãy phân tích UI và lập kế hoạch triển khai theo các giai đoạn build được độc lập. Phân biệt phần bám sát đề và phần mở rộng. Không sửa file, không thêm package, không commit hay push. Chờ tôi phê duyệt rồi mới triển khai.

---

**Ghi chú độ tin cậy:** Những nội dung được gắn tên WP04/WP05/WP06/WP07/WP15 là tóm lược các slide được cung cấp. Ví dụ kiến trúc ứng dụng, cách phân giai đoạn, checklist và prompt là **đề xuất vận dụng**, không được xem như yêu cầu nguyên văn của giảng viên. Ảnh Settings gốc không được nhúng trong tài liệu Markdown này.
