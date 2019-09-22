using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineer.Domain.Entities;

namespace Engineer.Domain.Repositories
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetClubsAsync();
        
        Task<Club> GetClubAsync(Guid id);

        void AddClub(Club entity);

        Task<bool> SaveChangesAsync();

        Task<bool> UpdateClub(Club entity);

        Task<bool> DeleteClub(Guid id);
    }
}