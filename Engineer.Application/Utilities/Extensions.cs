using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Engineer.Application.Utilities
{
    public static class Extensions
    {
        public static async Task<PaginatedResult<T>> paginate<T>(this IQueryable<T> source, int pageSize, int pageNumber)
        {
            return await new PaginatedResult<T>(pageNumber, pageSize).paginate(source);
        }
    }

    public class PaginatedResult<T> : ActionResult
    {
        private const int defaultPageSize = 5;
        private const int maxPageSize = 10;

        public int total { get; private set; }
        public int limit { get; private set; }
        public int page { get; private set; }
        public List<T> objects { get; private set; }

        internal PaginatedResult(int pageNumber = 0, int pageSize = defaultPageSize)
        {
            limit = pageSize;
            page = pageNumber;

            if (limit < 0 || limit > maxPageSize)
            {
                limit = defaultPageSize;
            }

            if (pageNumber < 0)
            {
                page = 0;
            }
        }

        internal async Task<PaginatedResult<T>> paginate(IQueryable<T> queryable)
        {
            total = queryable.Count();

            if (limit > total)
            {
                limit = total;
                page = 0;
            }

            var skip = page * limit;
            if ((skip + limit) > total)
            {
                skip = total - limit;
                page = total / limit - 1;
            }

            objects = await queryable.Skip(skip).Take(limit).ToListAsync();

            return this;
        }
    }
}