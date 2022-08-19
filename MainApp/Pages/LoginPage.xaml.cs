using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Models;

namespace MainApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = new LoginPageViewModel();
	}
}


public partial class LoginPageViewModel :ViewModelBase
{


    public UserLogin Model { get; set; } = new UserLogin();
    public LoginPageViewModel()
    {
        
    }

    [RelayCommand]
	public Task Login()
    {
        try
        {
           var result =  Account.LoginAsync(Model);
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    public Task Register()
    {
        Application.Current.MainPage = new RegisterPage();
        return Task.CompletedTask;
    }


}