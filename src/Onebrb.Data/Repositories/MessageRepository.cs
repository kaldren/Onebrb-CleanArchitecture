using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos;
using Onebrb.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Infrastructure.Repositories
{
    public class MessageRepository : EfRepository<Message, int>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Message>> GetAllArchivedMessagesAsync(int userId)
        {
            return await _dbContext.Messages
                                    .Where(x => x.AuthorId == userId && x.IsArchivedForAuthor)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetAllReceivedMessagesAsync(int userId)
        {
            return await _dbContext.Messages
                                    .Where(x => 
                                        x.RecipientId == userId
                                        && !x.IsArchivedForRecipient
                                        && !x.IsDeletedForRecipient)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetAllSentMessagesAsync(int userId)
        {
            return await _dbContext.Messages
                                    .Where(x =>
                                        x.AuthorId == userId
                                        && !x.IsArchivedForAuthor
                                        && !x.IsDeletedForAuthor)
                                    .ToListAsync();
        }
    }
}
