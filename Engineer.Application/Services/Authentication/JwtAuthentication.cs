using System;
using Microsoft.IdentityModel.Tokens;

namespace Engineer.Application.Services.Authentication
{
    public class JwtAuthentication
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
            
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(SecurityKey));

        public SigningCredentials SigningCredentials =>
            new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}