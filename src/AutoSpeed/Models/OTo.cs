namespace AutoSpeed.Models
{

/// <summary>
/// Lớp OTo kế thừa PhuongTien.
/// Trụ cột OOP: Inheritance + Polymorphism
/// </summary>
public class OTo : PhuongTien
{
    // ── Hằng số thuế (không dùng số ma thuật) ──────────────────────────────
    private const decimal THUE_TRUOC_BA_DUOI_9_CHO   = 0.12m;  // 12%
    private const decimal THUE_TIEU_THU_DAC_BIET      = 0.30m;  // 30%
    private const decimal THUE_TRUOC_BA_TREN_9_CHO    = 0.10m;  // 10%

    // ── Properties bổ sung ─────────────────────────────────────────────────
    private int _soChoNgoi;
    private double _dungTichDongCo;

    /// <summary>Số chỗ ngồi. Phải lớn hơn 0.</summary>
    public int SoChoNgoi
    {
        get => _soChoNgoi;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
            _soChoNgoi = value;
        }
    }

    /// <summary>Dung tích động cơ (lít/cc). Phải lớn hơn 0.</summary>
    public double DungTichDongCo
    {
        get => _dungTichDongCo;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
            _dungTichDongCo = value;
        }
    }

    // ── Constructor ─────────────────────────────────────────────────────────
    public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc,
               int soChoNgoi, double dungTichDongCo)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        SoChoNgoi = soChoNgoi;
        DungTichDongCo = dungTichDongCo;
    }

    // ── Override TinhGiaLanBanh (Đa hình) ──────────────────────────────────
    /// <summary>
    /// ≤ 9 chỗ: Giá + 12% (trước bạ) + 30% (tiêu thụ đặc biệt).
    /// > 9 chỗ: Giá + 10% (trước bạ).
    /// </summary>
    public override decimal TinhGiaLanBanh()
    {
        if (SoChoNgoi <= 9)
            return GiaGoc
                   + GiaGoc * THUE_TRUOC_BA_DUOI_9_CHO
                   + GiaGoc * THUE_TIEU_THU_DAC_BIET;
        else
            return GiaGoc + GiaGoc * THUE_TRUOC_BA_TREN_9_CHO;
    }

    // ── Override GetInfo (bổ sung thông tin xe hơi) ─────────────────────────
    public override string GetInfo() =>
        base.GetInfo() +
        $" | {SoChoNgoi} chỗ | Động cơ: {DungTichDongCo:F1}L";
}

}
