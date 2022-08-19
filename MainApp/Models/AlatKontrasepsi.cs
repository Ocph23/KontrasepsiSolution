using CommunityToolkit.Mvvm.ComponentModel;

namespace MainApp.Models
{
    public partial class AlatKontrasepsi : ObservableObject
    {
       [ObservableProperty] private int id;
       [ObservableProperty] private string nama;
       [ObservableProperty] private string keterangan;
        public string IdView => Id.ToString("D5");
    }
}
