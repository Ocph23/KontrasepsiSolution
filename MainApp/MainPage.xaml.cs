using CommunityToolkit.Mvvm.Input;
using MainApp.Pages;

namespace MainApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }

    }

    internal partial class MainPageViewModel:ViewModelBase
    {
        public MainPageViewModel()
        {
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
            Shell.Current.Navigation.PushAsync(new AddPelayananPage());
            return Task.CompletedTask;
        }


    }
}