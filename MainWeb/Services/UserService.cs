using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MainWeb.Data;

namespace MainWeb.Services
{

    public interface IUserAuthentification
    {
        Task<AuthenticateResponse> Authenticate(UserLogin model);
        Task<object> Profile();
    }

    public interface IUserService : IUserAuthentification
    {
        Task<string> AuthenticateUserProvider(IdentityUser user);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthenticateResponse> Authenticate(UserLogin model)
        {
            try
            {
                var password = GeneratePasswordHash(model.Password);

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var token = await GenerateJwtToken(user, userRoles);
                    return new AuthenticateResponse(user, token, userRoles.ToList());
                }

                throw new UnauthorizedAccessException($"Your Not Have Accout !");

            }
            catch (System.Exception ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
        }


        public async Task<string> AuthenticateUserProvider(IdentityUser user)
        {
            try
            {
                 var userRoles = await _userManager.GetRolesAsync(user);
                var token = await GenerateJwtToken(user,userRoles);
                if (string.IsNullOrEmpty(token))
                    throw new SystemException("You Not Have Access");
                return token;

            }
            catch (System.Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public Task<string> GenerateJwtToken(IdentityUser user, IList<string> userRoles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthorizationConstants.JWT_SECRET_KEY));

            var token = new JwtSecurityToken(
                issuer: AuthorizationConstants.JWT_ValidIssuer,
                audience: AuthorizationConstants.JWT_ValidAudience,
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        private static string GeneratePasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new SystemException("Password Requeired !");

            #pragma warning disable SYSLIB0021 // Type or member is obsolete
                        using var md5 = MD5CryptoServiceProvider.Create();
            #pragma warning restore SYSLIB0021 // Type or member is obsolete

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            //get hash result after compute it  
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            byte[] result = md5.Hash;
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            StringBuilder strBuilder = new ();
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            return strBuilder.ToString();
        }

        public Task<object> Profile()
        {
            throw new NotImplementedException();
        }

        // public async Task<object> Profile()
        // {
        //     var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        //     if (!string.IsNullOrEmpty(userName))
        //     {
        //         var user = await FindUserByUserName(userName);
        //         if(user != null)
        //         {
        //             var role = user.Roles.FirstOrDefault();

        //             if (role.Role.Name == "Administrator" || role.Role.Name == "Sales")
        //             {
        //                 return _context.Karyawan.Where(x => x.User.Id == user.Id).FirstOrDefault();
        //             }

        //             if (role.Role.Name == "Customer")
        //             {
        //                 return _context.Customer.Where(x => x.User.Id == user.Id).Include(x=>x.Karyawan).FirstOrDefault();
        //             }
        //         }
        //     }
        //         throw new UnauthorizedAccessException("You Are Profile Not Found !");
        // }
    }
}