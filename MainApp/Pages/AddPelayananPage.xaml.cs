    using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Collections.ObjectModel;

namespace MainApp.Pages;

public partial class AddPelayananPage : ContentPage
{
    public AddPelayananPage()
    {
        InitializeComponent();
        this.BindingContext = new AddPelayananViewModel();
    }
}

internal partial class AddPelayananViewModel : ViewModelBase
{

    private Pelayanan pelayanan;

    public Pelayanan Model
    {
        get { return pelayanan; }
        set { SetProperty(ref pelayanan, value); }
    }


    public AddPelayananViewModel()
    {
        Model = new Pelayanan();
        _ = Load();

    }

    public AddPelayananViewModel(Pelayanan model)
    {
        Model = model;
        _ = Load();
    }

    private async Task Load()
    {
        try
        {
            var alatKontrasepsi = await AlatKotrasepsi.Get();
            Alats.Clear();
            if (alatKontrasepsi != null)
            {
                foreach (var item in alatKontrasepsi)
                {
                    Alats.Add(item);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    [RelayCommand]
    Task Back()
    {
        Shell.Current.Navigation.PopAsync();
        return Task.CompletedTask;
    }


    [RelayCommand]
    async Task Save()
    {

        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            var success = false;
            if (Model.Id <= 0)
            {
                var data = await Pengajuan.PostAsync(Model);
                if (data != null)
                {
                    Model.Id = data.Id;
                    success = true;
                }
            }
            else
            {
                var data = await Pengajuan.PutsAsync(Model);
                if (data != null)
                {
                    success = true;
                }
            }

            if (success)
                await Shell.Current.DisplayAlert("Info", "Data Berhasil Disimpan !", "OK");
            else
            {
                throw new SystemException("Data Gagal Disimpan !");
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally { IsBusy = false; } 
    }

    public ObservableCollection<AlatKontrasepsi> Alats { get; set; } = new ObservableCollection<AlatKontrasepsi>();

}