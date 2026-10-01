namespace AutoSpeed.Models;

/// <summary>
/// Lớp trừu tượng cha — đại diện một phương tiện giao thông.
/// Trụ cột OOP: Encapsulation + Abstraction
/// </summary>
public abstract class PhuongTien
{
    // ── Private fields (Đóng gói) ──────────────────────────────────────────
    private string _maPT = "PT000";
    private string _tenHang = string.Empty;
    private int _namSanXuat;
    private decimal _giaGoc;

    // ── Properties có Validation ────────────────────────────────────────────

    /// <summary>Mã phương tiện. Chuỗi trống/khoảng trắng → mặc định "PT000".</summary>
    public string MaPT
    {
        get => _maPT;
        set => _maPT = string.IsNullOrWhiteSpace(value) ? "PT000" : value.Trim();
    }

    /// <summary>Tên hãng sản xuất. Không được để trống.</summary>
    public string TenHang
    {
        get => _tenHang;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên hãng không được để trống!");
            _tenHang = value.Trim();
        }
    }

    /// <summary>Năm sản xuất. Phải trong khoảng [1900 .. năm hiện tại].</summary>
    public int NamSanXuat
    {
        get => _namSanXuat;
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Năm sản xuất không hợp lệ!");
            _namSanXuat = value;
        }
    }

    /// <summary>Giá gốc (VNĐ). Phải lớn hơn 0.</summary>
    public decimal GiaGoc
    {
        get => _giaGoc;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Giá gốc phải lớn hơn 0!");
            _giaGoc = value;
        }
    }

    // ── Constructor ─────────────────────────────────────────────────────────
    /// <summary>
    /// Khởi tạo đầy đủ tham số. Gán qua property để validation luôn chạy.
    /// </summary>
    protected PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
    {
        MaPT = maPT;
        TenHang = tenHang;
        NamSanXuat = namSanXuat;    // ném ArgumentException nếu sai khoảng
        GiaGoc = giaGoc;
    }

    // ── Abstract Method (bắt buộc override — Đa hình) ──────────────────────
    /// <summary>Tính giá lăn bánh theo công thức riêng của từng loại xe.</summary>
    public abstract decimal TinhGiaLanBanh();

    // ── Virtual Method (có thể override để bổ sung thông tin) ──────────────
    /// <summary>Trả về thông tin cơ bản của phương tiện.</summary>
    public virtual string GetInfo() =>
        $"[{MaPT}] {TenHang} | Năm SX: {NamSanXuat} | Giá gốc: {GiaGoc:N0} VNĐ";
}
