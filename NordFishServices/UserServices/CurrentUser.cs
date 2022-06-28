using Microsoft.AspNetCore.Http;
using NordFish.Commons;
using NordFish.Database.Enumes;
using NordFish.Services.UserServices.Helpers;

namespace NordFish.Services.UserServices
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? Id
        {
            get
            {
                try
                {
                    if (_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(user => user.Type == Claims.ID)?.Value == null)
                    {
                        return null;
                    }
                    else
                    {
                        return long.Parse(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(user => user.Type == Claims.ID)?.Value);
                    }
                }
                catch (NullReferenceException ex)
                {
                    return null;
                }
            }
        }
        public string? Email
        {
            get
            {
                try
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(user => user.Type == Claims.EMAIL).Value;
                }
                catch (NullReferenceException ex)
                {
                    return null;
                }
            }
        }

        public bool? IsAuthenticated
        {
            get
            {
                try
                {
                    return _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated;
                }
                catch (NullReferenceException ex)
                {
                    return null;
                }
            }
        }
        public bool? IsAdmin
        {
            get
            {
                try
                {
                    return Role == UserTypes.Admin;
                }
                catch (NullReferenceException ex)
                {
                    return null;
                }
            }
        }
        public UserTypes? Role
        {
            get
            {
                try
                {
                    UserTypes? type = UserTypeHelper.GetUserTypeAsEnum(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(user => user.Type == Claims.ROLE)?.Value);
                    return type;
                }
                catch (NullReferenceException ex)
                {
                    return null;
                }
            }
        }
    }
}