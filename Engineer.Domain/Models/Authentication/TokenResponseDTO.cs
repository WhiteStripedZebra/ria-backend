using System;
using System.Collections.Generic;
using System.Text;

namespace Engineer.Domain.Models.Authentication
{
    public class TokenResponseDTO
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}
