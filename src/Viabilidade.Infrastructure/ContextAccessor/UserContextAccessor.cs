using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Viabilidade.Infrastructure.ContextAccessor
{
    public class UserContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string _userId;
        public string _userName;
        public UserContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            _userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
