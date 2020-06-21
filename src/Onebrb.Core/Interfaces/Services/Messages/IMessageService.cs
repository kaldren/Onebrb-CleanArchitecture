using Onebrb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Core.Interfaces.Services.Messages
{
    public interface IMessageService<TEntity>
    {
        Task<TEntity> GetMessageById(int id);
        Task<TEntity> CreateMessage(Message entity);
    }
}
