namespace AutoSpeed.Models;

/// <summary>
/// Lớp XeMay kế thừa PhuongTien.
/// Trụ cột OOP: Inheritance + Polymorphism
/// </summary>
public class XeMay : PhuongTien
{
    // ── Hằng số thuế ────────────────────────────────────────────────────────
    private const decimal THUE_TRUOC_BA_DUOI_175CC = 0.02m;  // 2%
    private const decimal THUE_TRUOC_BA_TU_175CC   = 0.05m;  // 5%
    private const int NGUONG_XYLANH = 175;                    // 175 cc

    // ── Property bổ sung ────────────────────────────────────────────────────
    private int _dungTichXylanh;

    /// <summary>Dung tích xy lanh (cc). Phải lớn hơn 0.</summary>
    public int DungTichXylanh
    {
        get => _dungTichXylanh;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Dung tích xy lanh phải lớn hơn 0!");
            _dungTichXylanh = value;
        }
    }

    // ── Constructor ─────────────────────────────────────────────────────────
    public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc,
                 int dungTichXylanh)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        DungTichXylanh = dungTichXylanh;
    }

    // ── Override TinhGiaLanBanh (Đa hình) ──────────────────────────────────
    /// <summary>
    /// < 175cc : Giá + 2% trước bạ.
    /// ≥ 175cc : Giá + 5% trước bạ.
    /// </summary>
    public override decimal TinhGiaLanBanh()
    {
        decimal tyLe = DungTichXylanh < NGUONG_XYLANH
            ? THUE_TRUOC_BA_DUOI_175CC
            : THUE_TRUOC_BA_TU_175CC;

        return GiaGoc + GiaGoc * tyLe;
    }

    // ── Override GetInfo ─────────────────────────────────────────────────────
    public override string GetInfo() =>
        base.GetInfo() + $" | Dung tích: {DungTichXylanh}cc";
}
