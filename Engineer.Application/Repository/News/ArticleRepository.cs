using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Application.Repository.News
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public void AddArticle(Article entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> GetArticleAsync(Guid id)
        {
            return await _context.Articles.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> UpdateArticle(Article entity)
        {
            throw new NotImplementedException();
        }
    }
}
