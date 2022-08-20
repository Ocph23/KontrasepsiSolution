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
    }

    public Peserta Model { get; private set; }


    [RelayCommand]
    Task Back()
    {
        Shell.Current.Navigation.PopAsync();
        return Task.CompletedTask;
    }


    [RelayCommand]
    Task Save()
    {
        return Task.CompletedTask;
    }
}