namespace MainWeb.Models
{
    public class ScheduleMessage
    {
        public int Id { get; set; }

        public int PesertaId { get; set; }

        public string Message { get; set; }

        public bool Sended { get; set; }

        public DateTime? Tanggal { get; set; }
        

    }
}
