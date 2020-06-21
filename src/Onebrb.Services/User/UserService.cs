using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos;
using Onebrb.Core.Interfaces.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.User
{
    public class UserService : IUserService<ApplicationUser>
    {
        private readonly IAsyncRepository<ApplicationUser, int> _userRepository;

        public UserService(IAsyncRepository<ApplicationUser, int> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string username)
        {
            return await _userRepository.FindSingleAsync(u => u.UserName == username);
        }
    }
}
