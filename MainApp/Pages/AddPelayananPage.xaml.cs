    using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Collections.ObjectModel;

namespace MainApp.Pages;

public partial class AddPelayananPage : ContentPage
{
    public AddPelayananPage()
    {
        InitializeComponent();
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
        _ = Load(Model);

    }

    public AddPelayananViewModel(Pelayanan model)
    {
        _ = Load(model);
    }

    private async Task Load(Pelayanan model)
    {
        try
        {
            Model = model;
            var alatKontraSelection = model.AlatKontrasepsiPilihan;
            var alatKontrasepsi = await AlatKotrasepsi.Get();
            Alats.Clear();
            if (alatKontrasepsi != null)
            {
                foreach (var item in alatKontrasepsi)
                {
                    Alats.Add(item);
                    if (alatKontraSelection!=null)
                    {
                        model.AlatKontrasepsiPilihan = alatKontraSelection;
                    }
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
    Task Pemeriksaan()
    {

        var page = new Pages.PemeriksaanPage();

        page.BindingContext = new PemeriksaanPageViewModel(Model);

        Shell.Current.Navigation.PushAsync(page);
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
