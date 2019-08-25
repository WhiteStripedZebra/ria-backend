using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Engineer.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Engineer.Application.IdentityHelpers
{
    public class EngineerUserManager<TUser> : UserManager<TUser> where TUser : EngineerUser
    {
        private readonly ApplicationDbContext _context;

        public EngineerUserManager(
            IUserStore<TUser> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<TUser> passwordHasher, 
            IEnumerable<IUserValidator<TUser>> userValidators, 
            IEnumerable<IPasswordValidator<TUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<TUser>> logger, 
            ApplicationDbContext context) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }
    }
}