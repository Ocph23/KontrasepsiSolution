namespace MainWeb.Models
{
    public class AlatKontrasepsi
    {
        public int Id { get; set; }
        public string Nama { get; set; } = string.Empty;
        public string Keterangan { get; set; } = string.Empty;
        public string IdView => Id.ToString("D5");
    }
}
