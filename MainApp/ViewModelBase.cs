using CommunityToolkit.Mvvm.ComponentModel;
using MainApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp
{
    public class ViewModelBase  :ObservableObject
    {
        public AccountService Account => DependencyService.Get<AccountService>();
        public AlatKontrasepsiService AlatKotrasepsi => DependencyService.Get<AlatKontrasepsiService>();
        
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title , value); }
        }


        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private bool isValidEmail;

        public bool IsValidEmail
        {
            get { return isValidEmail; }
            set { SetProperty(ref isValidEmail, value); }
        }


        private bool isVisibleError;

        public bool IsVisibleError
        {
            get { return isVisibleError; }
            set { SetProperty(ref isVisibleError , value); }
        }


        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set {SetProperty(ref isBusy , value); }
        }


    }
}
