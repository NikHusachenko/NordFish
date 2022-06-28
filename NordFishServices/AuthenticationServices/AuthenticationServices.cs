using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using NordFish.Commons;
using NordFish.Database.Entities;
using System.Security.Claims;

namespace NordFishServices.AuthenticationServices
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly HttpContext _httpContext;

        public AuthenticationServices(IHttpContextAccessor httpContextAccessor)
        {
             _httpContext = httpContextAccessor.HttpContext;
        }

        public async void SignOutAsync()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async void SignInAsync(UserEntity userEntity)
        {
            var claims = GetClaimsList(userEntity);
            var identity = GetClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public ClaimsIdentity GetClaimsIdentity(List<Claim> claims)
        {
            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public List<Claim> GetClaimsList(UserEntity userEntity)
        {
            return new List<Claim>()
            {
                 new Claim(Claims.ID, userEntity.Id.ToString()),
                 new Claim(Claims.EMAIL, userEntity.Email),
                 new Claim(Claims.LOGIN, userEntity.Login),
                 new Claim(Claims.ROLE, userEntity.Type.ToString()),
            };
        }
    }
}
