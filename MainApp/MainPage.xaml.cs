using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using MainApp.Pages;
using System.Text.Json;

namespace MainApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }

       
        protected override void OnDisappearing()
        {
            var collectionView = this.collection.SelectedItem=null;
            base.OnDisappearing();
        }
    }

    internal partial class MainPageViewModel:ViewModelBase
    {
        private Peserta peserta;

        public Peserta Model
        {
            get { return peserta; }
            set { SetProperty(ref peserta , value); }
        }


        [ObservableProperty]
        private string message;

        [ObservableProperty]
        private Pelayanan selectedItem;



        public MainPageViewModel()
        {
            _= Load();
        }

        private async Task Load()
        {
            try
            {
                var pesertaString = Preferences.Get("peserta", null);
                var peserta = JsonSerializer.Deserialize<Peserta>(pesertaString, Helper.JsonOptions);
                Model = await Peserta.GetAsync(peserta.Id);

                if(Model==null || Model.Pelayanan ==null || Model.Pelayanan.Count <= 0)
                    Message = "Silahkan Ajukan Pelayanan !";
                else 
                    Message=string.Empty; 
            }
            catch (Exception)
            {

                throw;
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
            if (SelectedItem != null)
            {
                var page = new AddPelayananPage();
                page.BindingContext = new AddPelayananViewModel(SelectedItem);
                Shell.Current.Navigation.PushAsync(page);
            }
                return Task.CompletedTask;
        }


    }
}