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
        Task<TEntity> DeleteMessage(int id, int userId);
        Task<IEnumerable<TEntity>> GetAllSentMessages(int userId);
        Task<IEnumerable<TEntity>> GetAllReceivedMessages(int userId);
        Task<IEnumerable<TEntity>> GetAllArchivedMessages(int userId);
        Task<IEnumerable<TEntity>> GetConversationWithUser(int currentUserId, int otherUserId);
    }
}
