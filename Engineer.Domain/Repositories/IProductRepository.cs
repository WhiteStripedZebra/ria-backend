using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineer.Domain.Entities;

namespace Engineer.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(Guid id);

        void AddProduct(Product entity);

        Task<bool> SaveChangesAsync();

        Task<bool> UpdateProduct(Product entity);

        Task<bool> DeleteProduct(Guid id);
    }
}