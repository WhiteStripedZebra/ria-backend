using System;
using Microsoft.IdentityModel.Tokens;

namespace Engineer.Application.Services.Authentication
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTimeOffset Expires => IssuedAt.Add(ValidFor);
        public DateTimeOffset NotBefore => DateTimeOffset.UtcNow;
        public DateTimeOffset IssuedAt => DateTimeOffset.UtcNow;
        public TimeSpan ValidFor { get; } = TimeSpan.FromMinutes(30);
        public Func<string> JtiGenerator => () => Guid.NewGuid().ToString();
        public SigningCredentials SigningCredentials { get; set; }
    }
}