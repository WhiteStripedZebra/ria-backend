using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Engineer.Domain.Enums;
using Engineer.Domain.Repositories;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Engineer.Application.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .OrderBy(order => order.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> FindPaged(int page, int pageSize)
        {
            return await _context.Orders.Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await _context.Orders
                .Include(order => order.Products)
                .ThenInclude(order => order.Product)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public void AddOrder(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreatedAt = DateTimeOffset.Now;
            entity.Status = OrderStatus.Appending;
            entity.OrderNumber =
                $"RIA-{entity.CreatedAt.Day}{entity.CreatedAt.Month}{entity.CreatedAt.Year}{entity.CreatedAt.Hour}{entity.CreatedAt.Minute}-{entity.UniversityId}";

            foreach (var item in entity.Products)
            {
                item.Product = _context.Products.FirstOrDefault(p => p.Id == item.Product.Id);
            }

            _context.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> UpdateOrder(Order entity)
        {
            _context.Update(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteOrder(Guid id)
        {
            var entity = await GetOrderAsync(id);

            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _context.Orders.Remove(entity);

            return await SaveChangesAsync();
        }
    }
}