using AutoSpeed.Models;

namespace AutoSpeed.Services
{

/// <summary>
/// Lớp quản lý tập hợp phương tiện giao thông.
/// Trụ cột OOP: Polymorphism — gọi TinhGiaLanBanh() qua kiểu cha PhuongTien.
/// </summary>
public class QuanLyPhuongTien
{
    private readonly List<PhuongTien> _danhSach = new();

    /// <summary>Thêm phương tiện mới vào danh sách.</summary>
    public void AddPhuongTien(PhuongTien pt)
    {
        ArgumentNullException.ThrowIfNull(pt);
        _danhSach.Add(pt);
    }

    /// <summary>In toàn bộ danh sách kèm giá lăn bánh ra console.</summary>
    public void DisplayAll()
    {
        if (_danhSach.Count == 0)
        {
            Console.WriteLine("  (Danh sách trống)");
            return;
        }

        Console.WriteLine($"\n{"STT",-4} {"Thông tin",-55} {"Giá lăn bánh",18}");
        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < _danhSach.Count; i++)
        {
            var pt = _danhSach[i];
            // Đây là Đa hình: gọi qua kiểu cha, C# tự dispatch đúng override
            Console.WriteLine($"{i + 1,-4} {pt.GetInfo(),-55} {pt.TinhGiaLanBanh(),18:N0} VNĐ");
        }
    }

    /// <summary>
    /// Tìm phương tiện có giá lăn bánh cao nhất.
    /// Trả về null nếu danh sách rỗng.
    /// </summary>
    public PhuongTien? FindMaxGiaLanBanh()
    {
        if (_danhSach.Count == 0) return null;

        // Đa hình: gọi TinhGiaLanBanh() qua kiểu cha, C# tự chọn đúng override
        return _danhSach.MaxBy(pt => pt.TinhGiaLanBanh());
    }

    /// <summary>
    /// Tìm phương tiện theo tên hãng (không phân biệt hoa thường).
    /// Dùng LINQ.
    /// </summary>
    public List<PhuongTien> SearchByName(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<PhuongTien>(_danhSach);   // trả toàn bộ nếu keyword rỗng

        return _danhSach
            .Where(pt => pt.TenHang.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>Số lượng phương tiện hiện có.</summary>
    public int Count => _danhSach.Count;
}

}
