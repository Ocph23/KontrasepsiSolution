using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MainApp.Pages;

public partial class KunjunganPage : ContentPage
{
	public KunjunganPage()
	{
		InitializeComponent();
	}
}



internal partial class KunjunganPageViewModel : ViewModelBase
{

    public ObservableCollection<KunjunganUlang> Datas { get; set; } = new ObservableCollection<KunjunganUlang>();

    private Pelayanan peserta;

    public Pelayanan Model
    {
        get { return peserta; }
        set { SetProperty(ref peserta, value); }
    }


    [ObservableProperty]
    private string message;

    [ObservableProperty]
    private KunjunganUlang selectedItem;



    public KunjunganPageViewModel(int id)
    {
        _ = Load(id);
    }

    private async Task Load(int pelayananId)
    {
        try
        {
            Model = await Pengajuan.GetAsync(pelayananId);

            if (Model == null || Model.Kunjungan== null || Model.Kunjungan.Count <= 0)
                Message = "Belum Ada Data Kunjungan Ulang !";
            else
            {
                Message = string.Empty;
                foreach (var item in Model.Kunjungan.OrderByDescending(x=>x.Id))
                {
                    Datas.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
           await Shell.Current.DisplayAlert("Error", ex.Message,"Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    Task Profile()
    {
        Shell.Current.Navigation.PushAsync(new ProfilePage());
        return Task.CompletedTask;
    }


    [RelayCommand]
    Task AddPelayanan()
    {
        var page = new AddPelayananPage();
        page.BindingContext = new AddPelayananViewModel();
        Shell.Current.Navigation.PushAsync(page);
        return Task.CompletedTask;
    }


    [RelayCommand]

    Task Edit(object model)
    {
        SelectedItem = model as KunjunganUlang;
        if (SelectedItem != null)
        {
            var page = new KunjunganDetailPage();
            page.BindingContext = new KunjunganDetailPageViewModel(SelectedItem);
            Shell.Current.Navigation.PushAsync(page);
        }
        return Task.CompletedTask;
    }

}