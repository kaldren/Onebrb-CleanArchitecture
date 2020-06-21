using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos.User;
using Onebrb.Infrastructure.Data;
using System.Threading.Tasks;

namespace Onebrb.Infrastructure.Repositories.User
{
    public class UserRepository : EfRepository<ApplicationUser, int>, IUserRepository<ApplicationUser>
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<ApplicationUser> GetUserByUserNameAsync(string username)
        {
            return _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == username);
        }
    }
}
