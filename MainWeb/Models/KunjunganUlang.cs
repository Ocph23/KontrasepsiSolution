namespace MainWeb.Models
{
    public class KunjunganUlang
    {
        public int Id { get; set; }
        public DateTime Tanggal  { get; set; } =DateTime.Now;
        public DateTime HaidTerakhir { get; set; } =DateTime.Now;   
        public double BeratBadan { get; set; }
        public string TekananDarah { get; set; } = string.Empty;
        public string KompilasiBerat { get; set; } =string.Empty;
        public string Kegagalan { get; set; } = string.Empty;
        public string PemeriksaanDanTindakan { get; set; } = string.Empty;
        public DateTime KonsultasiBerikut { get; set; } =DateTime.Now;
        public bool Datang { get; set; }
        public Petugas Petugas { get; set; }

    }
}
