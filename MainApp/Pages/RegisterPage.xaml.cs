using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Models;
using System.Text;

namespace MainApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterPageViewModel();
	}


	public partial class RegisterPageViewModel:ViewModelBase
	{
        public Peserta Model { get; set; } = new Peserta();

        public RegisterPageViewModel()
        {

            DataGender = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            DataPendidikan = Helper.GetPendidikan();
            DataJKN = Helper.GetJKN();
            DataPekerjaan = Helper.GetPekerjaan();

            PendidikanSelected = DataPendidikan.FirstOrDefault();
            PendidikanPasanganSelected = DataPendidikan.FirstOrDefault();

            PekerjaanSelected = DataPekerjaan.FirstOrDefault(); 
            PekerjaanPasanganSelected= DataPekerjaan.FirstOrDefault();

            RegisterCommand = new RelayCommand( async ()=> await RegisterAction(), RegisterValidate);
            Model.PropertyChanged += (_, __) => { 
                RegisterCommand = new RelayCommand( async ()=> await RegisterAction(), RegisterValidate);
            };
        }

        private async Task RegisterAction()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                if (IsValid())
                {
                    Peserta model = await Account.RegisterAsync(Model);
                    if (model != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Data Berhasil Disimpan !", "OK");
                        Application.Current.MainPage = new LoginPage();
                    }
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private bool IsValid()
        {
            StringBuilder sb = new StringBuilder();
            var valid = true;
            if(!IsValidEmail)
            { 
                valid = false;
                sb.Append("Email Tidak Valid !; ");
            }

            if (string.IsNullOrEmpty(Model.Nama) || string.IsNullOrEmpty(Model.NamaPasangan) 
                || string.IsNullOrEmpty(Model.Alamat)
                || string.IsNullOrEmpty(Model.TempatLahir)
                || string.IsNullOrEmpty(Model.NamaPasangan))
            {
                valid = false;
                sb.Append("Lengkapi Data !, Tidak Boleh Kosong ; ");
            }
            ErrorMessage = sb.ToString();
            IsVisibleError = !valid;

            return valid;
        }

        private bool RegisterValidate()
        {


            return true;

        }




        private RelayCommand registerCommand;

        public List<Gender> DataGender { get; }
        public List<EnumDisplay<Pendidikan>> DataPendidikan { get; }
        public List<EnumDisplay<JKN>> DataJKN { get; }
        public List<EnumDisplay<Pekerjaan>> DataPekerjaan { get; }

        public RelayCommand RegisterCommand
        {
            get { return registerCommand; }
            set { SetProperty(ref registerCommand , value); }
        }



        private EnumDisplay<Pendidikan> selectedPendidikan;
        public EnumDisplay<Pendidikan> PendidikanSelected
        {
            get { return selectedPendidikan; }
            set
            {
                SetProperty(ref selectedPendidikan, value);
                if (value != null)
                    Model.Pendidikan = value.Value;
            }
        }


        private EnumDisplay<Pekerjaan> pekerjaanSelected
            ;
        public EnumDisplay<Pekerjaan> PekerjaanSelected
        {
            get { return pekerjaanSelected; }
            set
            {
                SetProperty(ref pekerjaanSelected, value);
                if (value != null)
                    Model.Pekerjaan = value.Value;
            }
        }


        public EnumDisplay<JKN> JKNSelected
        {
            get { return jknSelected; }
            set
            {
                SetProperty(ref jknSelected, value);
                if (value != null)
                    Model.JKN = value.Value;
            }
        }



        private EnumDisplay<Pendidikan> pendidikanPasanganSelected;
        public EnumDisplay<Pendidikan> PendidikanPasanganSelected
        {
            get { return pendidikanPasanganSelected; }
            set
            {
                SetProperty(ref pendidikanPasanganSelected, value);
                if (value != null)
                    Model.PendidikanPasangan = value.Value;
            }
        }


        private EnumDisplay<Pekerjaan> pekerjaanPasanganSelected
            ;
        private EnumDisplay<JKN> jknSelected;

        public EnumDisplay<Pekerjaan> PekerjaanPasanganSelected
        {
            get { return pekerjaanPasanganSelected; }
            set
            {
                SetProperty(ref pekerjaanPasanganSelected, value);
                if (value != null)
                    Model.PekerjaanPasangan= value.Value;
            }
        }


    }
}