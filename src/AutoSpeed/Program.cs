using AutoSpeed.Models;
using AutoSpeed.Services;
using System.Globalization;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║   HỆ THỐNG QUẢN LÝ PHƯƠNG TIỆN — AUTOSPEED LOGISTICS    ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");

// ── DEMO TEST CASES ─────────────────────────────────────────────────────────

int pass = 0, fail = 0;

void PrintResult(string tcId, bool ok, string mo_ta)
{
    string trangThai = ok ? "[PASS] ✅" : "[FAIL] ❌";
    Console.WriteLine($"  {trangThai}  {tcId}: {mo_ta}");
    if (ok) pass++; else fail++;
}

Console.WriteLine("── KIỂM THỬ TỪNG TEST CASE ──────────────────────────────────\n");

// TC01 — Validation năm sản xuất
try
{
    var _ = new OTo("PT001", "Toyota", 1850, 1_000_000_000m, 5, 2.0);
    PrintResult("TC01", false, "Lẽ ra phải ném ArgumentException nhưng không ném");
}
catch (ArgumentException ex) when (ex.Message == "Năm sản xuất không hợp lệ!")
{
    PrintResult("TC01", true, $"ArgumentException đúng message: \"{ex.Message}\"");
}
catch (Exception ex)
{
    PrintResult("TC01", false, $"Sai loại exception hoặc message: {ex.Message}");
}

// TC02 — Giá lăn bánh ô tô 5 chỗ, giá gốc 1 tỷ
try
{
    var oto5cho = new OTo("PT002", "Toyota Camry", 2022, 1_000_000_000m, 5, 2.5);
    decimal gia = oto5cho.TinhGiaLanBanh();
    decimal kyVong = 1_420_000_000m;
    PrintResult("TC02", gia == kyVong,
        $"Giá lăn bánh = {gia:N0} VNĐ (kỳ vọng {kyVong:N0} VNĐ)");
}
catch (Exception ex) { PrintResult("TC02", false, ex.Message); }

// TC03 — Giá lăn bánh xe máy 150cc, giá gốc 50 triệu
try
{
    var xemay150 = new XeMay("PT003", "Honda Wave", 2023, 50_000_000m, 150);
    decimal gia = xemay150.TinhGiaLanBanh();
    decimal kyVong = 51_000_000m;
    PrintResult("TC03", gia == kyVong,
        $"Giá lăn bánh = {gia:N0} VNĐ (kỳ vọng {kyVong:N0} VNĐ)");
}
catch (Exception ex) { PrintResult("TC03", false, ex.Message); }

// TC04 — Đa hình List<PhuongTien>
try
{
    var oto   = new OTo("PT004", "Ford Transit", 2021, 800_000_000m, 16, 2.0);
    var xemay = new XeMay("PT005", "Yamaha Exciter", 2023, 55_000_000m, 155);

    List<PhuongTien> dsPT = new() { oto, xemay };
    bool tatCaDung = true;

    foreach (var pt in dsPT)
    {
        decimal gia = pt.TinhGiaLanBanh();   // Đa hình: gọi qua kiểu cha
        if (gia <= 0) tatCaDung = false;
        Console.WriteLine($"    → {pt.GetType().Name}: {pt.TenHang} — Giá lăn bánh: {gia:N0} VNĐ");
    }
    PrintResult("TC04", tatCaDung, "Đa hình hoạt động đúng với List<PhuongTien>");
}
catch (Exception ex) { PrintResult("TC04", false, ex.Message); }

// TC05 — FindMaxGiaLanBanh trả đúng ô tô 5 chỗ 1.42 tỷ
try
{
    var ql = new QuanLyPhuongTien();
    var oto5  = new OTo("PT006", "Toyota Vios", 2022, 1_000_000_000m, 5, 1.5);   // 1.42 tỷ
    var xemay = new XeMay("PT007", "Honda SH", 2023, 80_000_000m, 160);

    ql.AddPhuongTien(oto5);
    ql.AddPhuongTien(xemay);

    var maxPT = ql.FindMaxGiaLanBanh();
    bool ok = maxPT is OTo oMax && oMax.MaPT == "PT006";
    PrintResult("TC05", ok,
        $"Phương tiện giá cao nhất: {maxPT?.TenHang} — {maxPT?.TinhGiaLanBanh():N0} VNĐ");
}
catch (Exception ex) { PrintResult("TC05", false, ex.Message); }

// ── TỔNG KẾT TEST ───────────────────────────────────────────────────────────
Console.WriteLine($"\n── KẾT QUẢ: {pass} PASS / {fail} FAIL / 5 TOTAL ──────────────────\n");

// ── DEMO DISPLAYALL ─────────────────────────────────────────────────────────
Console.WriteLine("── DEMO DISPLAYALL ──────────────────────────────────────────");
var heThong = new QuanLyPhuongTien();
heThong.AddPhuongTien(new OTo("OT001", "Toyota Camry",    2022, 1_000_000_000m, 5,  2.5));
heThong.AddPhuongTien(new OTo("OT002", "Ford Transit",    2021,   800_000_000m, 16, 2.0));
heThong.AddPhuongTien(new XeMay("XM001", "Honda Wave",    2023,    50_000_000m, 150));
heThong.AddPhuongTien(new XeMay("XM002", "Yamaha Exciter",2023,    55_000_000m, 155));
heThong.AddPhuongTien(new XeMay("XM003", "Honda SH 350",  2024,   125_000_000m, 350));

heThong.DisplayAll();

// FindMax
var maxXe = heThong.FindMaxGiaLanBanh();
Console.WriteLine($"\n🏆 Giá lăn bánh cao nhất: {maxXe?.TenHang} — {maxXe?.TinhGiaLanBanh():N0} VNĐ");

// SearchByName
Console.WriteLine("\n🔍 Tìm kiếm 'Honda':");
var ketQua = heThong.SearchByName("Honda");
foreach (var xe in ketQua)
    Console.WriteLine($"   → {xe.GetInfo()}");
