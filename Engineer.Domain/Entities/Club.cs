using System;

namespace Engineer.Domain.Entities
{
    public class Club
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string ContactMail { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
