using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineer.Domain.Entities;

namespace Engineer.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> GetOrderAsync(Guid id);

        void AddOrder(Order entity);

        Task<bool> SaveChangesAsync();

        Task<bool> UpdateOrder(Order entity);

        Task<bool> DeleteOrder(Guid id);
    }
}