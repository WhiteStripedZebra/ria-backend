using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Engineer.Application.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetClubsAsync()
        {
            return await _context.Clubs.OrderBy(club => club.Name).ToListAsync();
        }

        public Task<Club> GetClubAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AddClub(Club entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateClub(Club entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClub(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}