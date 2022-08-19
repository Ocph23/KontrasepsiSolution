using CommunityToolkit.Mvvm.ComponentModel;

namespace MainApp.Models
{
    public partial class Petugas :ObservableObject
    {
        [ObservableProperty]public int id;
        [ObservableProperty]public string nama ;
        [ObservableProperty]public string email ;
        [ObservableProperty]public string jabatan ;    
        [ObservableProperty]public string alamat ;    
        [ObservableProperty]public string telepon ;
        [ObservableProperty]public string userId ;
    }
}
