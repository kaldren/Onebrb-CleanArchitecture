using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos;
using Onebrb.Core.Interfaces.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Messages
{
    public class MessageService : IMessageService<Message>
    {
        private readonly IAsyncRepository<Message, int> _messageRepository;

        public MessageService(IAsyncRepository<Message, int> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Message> CreateMessage(Message entity)
        {
            await _messageRepository.AddAsync(entity);
            await _messageRepository.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Message>> GetAllReceivedMessages(int userId)
        {
            var result = await _messageRepository.FindAsync(x => x.RecipientId == userId);

            if (result == null)
            {
                return Enumerable.Empty<Message>();
            }

            return result;
        }

        public async Task<IEnumerable<Message>> GetAllSentMessages(int userId)
        {
            var result = await _messageRepository.FindAsync(x => x.AuthorId == userId);

            if (result == null)
            {
                return Enumerable.Empty<Message>();
            }

            return result;
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _messageRepository.GetAsync(id);
        }
    }
}
