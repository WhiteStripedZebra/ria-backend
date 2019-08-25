using System;
using Engineer.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Engineer.Domain.Entities
{
    public class EngineerUser : IdentityUser
    {
        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public CouncilRole Role { get; set; }

        public DateTimeOffset LastLoginTimestamp { get; set; }
    }
}