using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Text.Json;

namespace MainApp.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
		BindingContext = new ProfilePageViewModel();
	}
}

public partial class ProfilePageViewModel:ViewModelBase
{
    public ProfilePageViewModel()
    {
        DataPekerjaan = Helper.GetPekerjaan();

        var modelString = Preferences.Get("peserta", null);

		if (modelString == null)
        {
            Shell.Current.DisplayAlert("Info", "Silahkan Logout Dan Masuk Kembali", "OK");
			Shell.Current.Navigation.PopAsync();
        }
        else
        {
            Model = JsonSerializer.Deserialize<Peserta>(modelString, Helper.JsonOptions);
        }

        _=LoadPicker();

    }

    private async Task LoadPicker()
    {
        await Task.Delay(200);
        PekerjaanIndex = (int)Model.Pekerjaan;
        PekerjaanPasanganIndex = (int)Model.PekerjaanPasangan;
    }

    public Peserta Model { get; private set; }

    public List<EnumDisplay<Pekerjaan>> DataPekerjaan { get; }


    public bool PekerjaanPasanganLainShow => Model.PekerjaanPasangan == Pekerjaan.LainLain ? true : false;
    public bool PekerjaanLainShow => Model.Pekerjaan == Pekerjaan.LainLain ? true : false;


    private EnumDisplay<Pekerjaan> pekerjaanSelected
;
    public EnumDisplay<Pekerjaan> PekerjaanSelected
    {
        get { return pekerjaanSelected; }
        set
        {
            SetProperty(ref pekerjaanSelected, value);
            if (value != null)
                Model.Pekerjaan = value.Value;
            OnPropertyChanged("PekerjaanLainShow");
        }
    }


    private EnumDisplay<Pekerjaan> pekerjaanPasanganSelected;

    public EnumDisplay<Pekerjaan> PekerjaanPasanganSelected
    {
        get { return pekerjaanPasanganSelected; }
        set
        {
            SetProperty(ref pekerjaanPasanganSelected, value);
            if (value != null)
                Model.PekerjaanPasangan = value.Value;
            OnPropertyChanged("PekerjaanPasanganLainShow");
        }
    }

    private int pekerjaanPasanganIndex;

    public int PekerjaanPasanganIndex
    {
        get { return pekerjaanPasanganIndex; }
        set { SetProperty(ref pekerjaanPasanganIndex , value); }
    }



    private int pekerjaanIndex;

    public int PekerjaanIndex
    {
        get { return pekerjaanIndex; }
        set { SetProperty(ref pekerjaanIndex, value); }
    }



    [RelayCommand]
    Task Back()
    {
        Shell.Current.Navigation.PopAsync();
        return Task.CompletedTask;
    }


    [RelayCommand]
    Task Save()
    {
        PekerjaanPasanganIndex = (int)Model.PekerjaanPasangan;

        return Task.CompletedTask;
    }
}