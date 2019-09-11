using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Engineer.Application.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                .OrderBy(product => product.Name)
                .ToListAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public void AddProduct(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreatedAt = DateTimeOffset.Now;

            _context.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> UpdateProduct(Product entity)
        {
            _context.Update(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var entity = await GetProductAsync(id);

            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _context.Products.Remove(entity);

            return await SaveChangesAsync();
        }


    }
}
