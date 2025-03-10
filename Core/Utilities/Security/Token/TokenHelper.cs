using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Token
{
    public class TokenHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTokenFromHeader()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            return token?.Replace("Bearer ", "").Trim(); // "Bearer " kısmını temizle
        }
    }
}