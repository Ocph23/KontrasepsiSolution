using CommunityToolkit.Mvvm.ComponentModel;

namespace MainApp.Models
{
    public partial class KunjunganUlang : ObservableObject
    {
        [ObservableProperty] private int id;
        [ObservableProperty] private DateTime tanggal = DateTime.Now;
        [ObservableProperty] private DateTime haidTerakhir = DateTime.Now;
        [ObservableProperty] private double beratBadan;
        [ObservableProperty] private string tekananDarah;
        [ObservableProperty] private string kompilasiBerat;
        [ObservableProperty] private string kegagalan;
        [ObservableProperty] private string pemeriksaanDanTindakan;
        [ObservableProperty] private DateTime konsultasiBerikut = DateTime.Now;
        [ObservableProperty] private bool datang;
        [ObservableProperty] private Petugas petugas;

    }
}
