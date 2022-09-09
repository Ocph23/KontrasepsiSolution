using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MainApp.Models
{
    public partial class Peserta  :ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nama;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private Gender jenisKelamin ;

        [ObservableProperty]
        private Pendidikan pendidikan ;
        [ObservableProperty]
        private Pekerjaan pekerjaan;

        [ObservableProperty]
        private DateTime? tanggalLahir = DateTime.Now.AddYears(-17);

        [ObservableProperty]
        private string tempatLahir ; 

        [ObservableProperty]
        private string namaPasangan ;

        public string tahapanKS { get; set; } = string.Empty;

        public JKN JKN { get; set; }



        [ObservableProperty]
        private Pendidikan pendidikanPasangan ;
        
        [ObservableProperty]
        private Pekerjaan pekerjaanPasangan ;

        [ObservableProperty]
        private string alamat ; 
        
        [ObservableProperty]
        private ICollection<Pelayanan>? pelayanan ;

        [ObservableProperty]
        private string? deviceToken ;
        
        [ObservableProperty]
        private string? userId ; 




        public string PendidikanDisplay
        {
            get
            {
                return this.Pendidikan.ToStringText();
            }
        }


    }
}
