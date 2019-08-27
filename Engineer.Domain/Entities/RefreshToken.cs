namespace Engineer.Domain.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
        public string UserId { get; set; }
    }
}