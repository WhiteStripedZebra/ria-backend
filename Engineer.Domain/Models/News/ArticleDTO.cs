using System;
using System.Collections.Generic;
using System.Text;

namespace Engineer.Domain.Models.News
{
    public class ArticleDTO
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public String Tags { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }
    }
}
