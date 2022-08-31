using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Collections.ObjectModel;

namespace MainApp.Pages;

public partial class PemeriksaanPage : ContentPage
{
	public PemeriksaanPage()
	{
		InitializeComponent();
	}
}




internal partial class PemeriksaanPageViewModel :ViewModelBase
{
    private Pelayanan model;

    public List<KeadaanUmum> Keadaans { get; }
    public List<PosisiRahim> Rahims { get; }
    public ObservableCollection<AlatKontrasepsi> Alats { get; set; } = new ObservableCollection<AlatKontrasepsi>();


    public PemeriksaanPageViewModel(Pelayanan model)
    {
        this.model = model;
        Keadaans = Enum.GetValues(typeof(KeadaanUmum)).Cast<KeadaanUmum>().ToList();
        Rahims = Enum.GetValues(typeof(PosisiRahim)).Cast<PosisiRahim>().ToList();
        var alatKontrasepsi = AlatKotrasepsi.Get().Result;
        Alats.Clear();
        if (alatKontrasepsi != null)
        {
            foreach (var item in alatKontrasepsi)
            {
                Alats.Add(item);
                if (model.AlatKontrasepsiPilihan != null && model.AlatKontrasepsiPilihan.Id == item.Id)
                {
                    model.AlatKontrasepsiPilihan = item;
                }
            }
        }

    }


    [RelayCommand]
    Task Back()
    {
        Shell.Current.Navigation.PopAsync();
        return Task.CompletedTask;
    }
}