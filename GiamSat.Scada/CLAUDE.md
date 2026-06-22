# GiamSat.Scada

WinForms app (.NET) hiển thị biểu đồ TORQUE thời gian thực từ EasyScada driver,
dùng LiveCharts để vẽ chart và MigraDoc để xuất báo cáo PDF.

## Change log

### 2026-06-22 — Resize & responsive Form1 cho màn hình 1024×768
- Mục tiêu: form trước đây thiết kế cho `ClientSize 1264×808`, vượt quá màn hình
  độ phân giải 1024×768. Cần thu nhỏ và làm layout co giãn theo kích thước cửa sổ.
- Thay đổi trong [Form1.Designer.cs](Form1.Designer.cs):
  - `ClientSize` 1264×808 → **1000×680**; thêm `MinimumSize = 820×600`;
    `MaximizeBox = false` → **true** (cho phép phóng to toàn màn hình).
  - Header `label1`: 1267×64 → 1000×50, font 20 → 18, `Anchor = Top|Left|Right`.
  - `_tab`: vị trí/size gọn lại (12,134) 976×512, `Anchor = Top|Bottom|Left|Right`
    để giãn theo form. Hai `tabPage` size theo đó.
  - `_chart1`/`_chart2`: đặt `Dock = Fill` để tự lấp đầy tab page.
  - Cụm nút phải (`_btnStartStop`, `_btnUpdate`, `_btnExport`): thu nhỏ font/size,
    `Anchor = Top|Right` để bám mép phải.
  - Cụm label/textbox Max/Target/Torque (row 1 & 2): dồn lên dải header,
    thu gọn toạ độ x để nằm gọn bên trái nút Start/Stop.
  - Thanh dưới: `label4` + `_labSriverStatus` `Anchor = Bottom|Left`;
    `_labTime` `Anchor = Bottom|Right`; font 12 → 10.
- Kết quả: layout vừa khít 1024×768, đồng thời co giãn (responsive) khi resize
  hoặc maximize cửa sổ. Build Debug pass, không lỗi.

### 2026-06-22 — Resize form Settings cho màn hình 1024×768
- Form Settings (dialog `ShowDialog`) trước đây `ClientSize 1056×375`, rộng hơn
  màn hình 1024. Là lưới 3 cột field cố định nên chỉ cần thu gọn bề ngang.
- Thay đổi trong [Settings.Designer.cs](Settings.Designer.cs) (chỉ designer,
  **giữ nguyên logic** trong [Settings.cs](Settings.cs)):
  - `ClientSize` 1056×375 → **980×375**.
  - Bề rộng các `TextBox` 317 → 300; nút `_btnSave` 317 → 300.
  - Dồn 3 cột: x = 30 → **20**, 368 → **340**, 717 → **660**
    (cột 3 kết thúc tại 960, lề phải 20).
  - Giữ nguyên toạ độ y, font, thứ tự tab và toàn bộ field.
- Build Debug pass, không lỗi.

## Build

```
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" GiamSat.Scada.csproj /t:Build /p:Configuration=Debug
```
