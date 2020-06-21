using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Onebrb.Core.Entities;
using Onebrb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onebrb.Infrastructure.Entities
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserManager(
            ApplicationDbContext db,
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<ApplicationUserManager> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _db = db;
        }

        public async Task<bool> SetFirstNameAsync(ApplicationUser user, string firstName)
        {
            if (user == null)
            {
                return false;
            }

            var userFromDb = await _db.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (userFromDb == null)
            {
                return false;
            }

            userFromDb.FirstName = firstName;
            _db.Users.Update(userFromDb);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<string> GetFirstNameAsync(ApplicationUser user)
        {
            var userFromDb = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            return userFromDb.FirstName;
        }

        public async Task<bool> SetLastNameAsync(ApplicationUser user, string firstName)
        {
            if (user == null)
            {
                return false;
            }

            var userFromDb = await _db.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (userFromDb == null)
            {
                return false;
            }

            userFromDb.LastName = firstName;
            _db.Users.Update(userFromDb);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<string> GetLastNameAsync(ApplicationUser user)
        {
            var userFromDb = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            return userFromDb.LastName;
        }
    }
}
