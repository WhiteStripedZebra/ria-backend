using Engineer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Domain.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync();

        Task<Article> GetArticleAsync(Guid id);

        void AddArticle(Article entity);

        Task<bool> SaveChangesAsync();

        Task<bool> UpdateArticle(Article entity);

        Task<bool> DeleteArticle(Guid id);
    }
}
