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

internal partial class AddPelayananViewModel  :ViewModelBase
{

    private Pelayanan pelayanan;

    public Pelayanan Model
    {
        get { return pelayanan; }
        set { SetProperty(ref pelayanan , value); }
    }


    public AddPelayananViewModel()
    {
        Model = new Pelayanan();
		_=Load();

    }

    private async Task Load()
    {
        try
        {
            var alatKontrasepsi = await AlatKotrasepsi.Get();
            Alats.Clear();
            if(alatKontrasepsi!=null)
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
    Task Save()
    {
        return Task.CompletedTask;
    }

    public ObservableCollection<AlatKontrasepsi> Alats { get; set; } = new  ObservableCollection<AlatKontrasepsi>();

}