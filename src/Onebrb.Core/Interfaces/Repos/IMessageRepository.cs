using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Core.Interfaces.Repos
{
    public interface IMessageRepository : IAsyncRepository<Message, int>
    {
        Task<IEnumerable<Message>> GetAllReceivedMessagesAsync(int userId);
        Task<IEnumerable<Message>> GetAllSentMessagesAsync(int userId);
        Task<IEnumerable<Message>> GetAllArchivedMessagesAsync(int userId);
    }
}
