using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Models
{
    public partial class UserLogin :ObservableObject
    {
        [ObservableProperty] private string userName;
        [ObservableProperty] private string password;
    }

    public class AuthenticateResponse
    {

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; }
    }
}
