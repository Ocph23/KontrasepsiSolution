using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Collections.ObjectModel;

namespace MainApp.Pages;

public partial class KunjunganDetailPage : ContentPage
{
	public KunjunganDetailPage()
	{
		InitializeComponent();
	}
}




internal partial class KunjunganDetailPageViewModel :ViewModelBase
{
    public KunjunganUlang Model { get; set; }

    public KunjunganDetailPageViewModel(KunjunganUlang model)
    {
        this.Model = model;
    }


    [RelayCommand]
    Task Back()
    {
        Shell.Current.Navigation.PopAsync();
        return Task.CompletedTask;
    }
}