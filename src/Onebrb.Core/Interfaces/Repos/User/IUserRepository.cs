using Onebrb.Core.Dtos.User;
using Onebrb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Core.Interfaces.Repos.User
{
    public interface IUserRepository<TEntity> : IAsyncRepository<TEntity, int> where TEntity : class
    {
        Task<TEntity> GetUserByUserNameAsync(string username);
        Task<TEntity> GetUserByIdAsync(int id);
    }
}
