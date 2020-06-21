using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Core.Interfaces.Services.User
{
    public interface IUserService<TEntity>
    {
        Task<TEntity> GetUserByIdAsync(int id);
        Task<TEntity> GetUserByUserNameAsync(string username);
    }
}
