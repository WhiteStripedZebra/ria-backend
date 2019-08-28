using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineer.Domain.Entities
{
    public class RefreshToken
    {

        [Key]
        public string Token { get; set; }
        public long Expiration { get; set; }
        public string UserId { get; set; }
    }
}