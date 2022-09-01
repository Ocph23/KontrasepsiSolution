using CommunityToolkit.Mvvm.ComponentModel;
using MainApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainApp.Models
{
    public partial class Pelayanan     :ObservableObject
    {
        public int Id { get; set; }

        public int AnakHidupLaki { get; set; }
        public int AnakHidupPerempuan { get; set; }
        public int UmurAnakTerkecilTahun { get; set; }
        public int UmurAnakTerkecilBulan { get; set; }
        public bool StatusKB { get; set; } = true;
        public AlatKontrasepsi? CaraKBTerakhir { get; set; }
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
        public string TekananDarah { get; set; } = String.Empty;
        public PosisiRahim PosisiRahim { get; set; }

        private AlatKontrasepsi? alatKontrasepsiPilihan;

        public AlatKontrasepsi? AlatKontrasepsiPilihan
        {
            get { return alatKontrasepsiPilihan; }
            set {SetProperty(ref alatKontrasepsiPilihan , value); }
        }

        public DateTime? TanggalDilayani { get; set; }
        public DateTime? TanggalDicabut { get; set; }
        public DateTime? TanggalKembali { get; set; }
        public string PenanggungJawab { get; set; } = string.Empty;
        public ICollection<KunjunganUlang>? Kunjungan { get; set; }
        public Petugas? Petugas { get; set; }
        public bool Radang { get; set; }
        public bool KeganasanGinekologi { get; set; }
        public bool Diabetes { get; set; }
        public bool PemekuanDarah { get; set; }
        public bool RadangOrchitis { get; set; }
        public bool KeganasanGinekologi2 { get; set; }

        public DateTime Tanggal { get; set; } = DateTime.Now;

        public int PesertaId { get; set; }

        [NotMapped]
        public string? NamaPeserta { get; set; } = string.Empty;


        [NotMapped]
        public string? AlamatPeserta { get; set; } = string.Empty;

    }
}