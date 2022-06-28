using NordFish.Database.Entities;
using System.Security.Claims;

namespace NordFishServices.AuthenticationServices
{
    public interface IAuthenticationServices
    {
        public void SignInAsync(UserEntity userEntity);
        public void SignOutAsync();
        List<Claim> GetClaimsList(UserEntity userEntity);
        ClaimsIdentity GetClaimsIdentity(List<Claim> claims);
    }
}
