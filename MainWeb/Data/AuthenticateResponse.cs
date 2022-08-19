using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace MainWeb.Data
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(){}

        public AuthenticateResponse(IdentityUser user, string token, List<string> roles)
        {
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.Token = token;
            this.Roles = roles;
        }

        public string UserName { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string Token { get; set; }=string.Empty;
        public IEnumerable<string> Roles { get; set; }
    }


    public class UserLogin
    {
        public string UserName { get;  set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
    }
}