using MainApp.Models;

namespace MainApp.Models
{
    public partial class Pelayanan
    {
        public int Id { get; set; }
        public DateTime TerakhirHaid { get; set; } = DateTime.Now;
        public bool Hamil { get; set; }
        public bool Menyusui { get; set; }
        public int JumlahKehamilan { get; set; }
        public int JumlahPersalinan { get; set; }
        public int JumlahKeguguran { get; set; }
        public bool SakitKuning { get; set; }
        public bool PendarahanPervaginam { get; set; }
        public bool KeputihanYangLama { get; set; }
        public bool Tumor { get; set; }
        public KeadaanUmum Keadaan { get; set; }
        public double BeratBadan { get; set; }
        public double TekananDarah { get; set; }
        public PosisiRahim PosisiRahim { get; set; }
        public AlatKontrasepsi? AlatKontrasepsiPilihan { get; set; }
        public DateTime? TanggalDilayani { get; set; }
        public DateTime? TanggalDicabut { get; set; }
        public string PenanggungJawab { get; set; } = string.Empty;
        public ICollection<KunjunganUlang>? Kunjungan { get; set; }
        public Petugas? Petugas { get; set; }


        public bool Radang { get; set; }
        public bool KeganasanGinekologi { get; set; }
        public bool Diabetes { get; set; }
        public bool PemekuanDarah { get; set; }
        public bool RadangOrchitis { get; set; }
        public bool KeganasanGinekologi2 { get; set; }

    }
}