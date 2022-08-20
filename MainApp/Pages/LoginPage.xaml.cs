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
        Model.Password = "Rl8)=1@E^";
    }

    [RelayCommand]
	public async Task Login()
    {
        try
        {
           var success = await  Account.LoginAsync(Model);
           if(success)
            {
                Application.Current.MainPage = new AppShell();
                return;
            }

            throw new SystemException("Maaf Anda Tidak Memiliki Akses !");
        }
        catch (Exception ex)
        {
           await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    public Task Register()
    {
        Application.Current.MainPage = new RegisterPage();
        return Task.CompletedTask;
    }


}