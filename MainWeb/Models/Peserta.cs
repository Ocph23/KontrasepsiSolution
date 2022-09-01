namespace MainWeb.Models
{
    public class Peserta
    {
        public int Id { get; set; }

        public string Nama { get; set; }=string.Empty;
        public string Email{ get; set; }=string.Empty;

        public Gender JenisKelamin { get; set; }

        public Pendidikan Pendidikan { get; set; }
        public Pekerjaan Pekerjaan{ get; set; }

        public DateTime? TanggalLahir { get; set; } = DateTime.Now.AddYears(-17);

        public string TempatLahir { get; set; } = string.Empty;

        public string NamaPasangan { get; set; } = string.Empty;
        public string TahapanKS { get; set; } = string.Empty;
        public JKN JKN { get; set; }
        public Pendidikan PendidikanPasangan { get; set; }
        public Pekerjaan PekerjaanPasangan { get; set; }

        public string Alamat { get; set; } = string.Empty;
        public ICollection<Pelayanan>? Pelayanan { get; set; }
        public string? DeviceToken { get; set; }
        public string? UserId { get; set; } 

    }
}
