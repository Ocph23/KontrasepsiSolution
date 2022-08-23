namespace MainApp.Pages;

public partial class AddPelayananPage : ContentPage
{
	public AddPelayananPage()
	{
		InitializeComponent();
		this.BindingContext = new AddPelayananViewModel();
	}
}

internal class AddPelayananViewModel
{
    public AddPelayananViewModel()
    {
    }
}