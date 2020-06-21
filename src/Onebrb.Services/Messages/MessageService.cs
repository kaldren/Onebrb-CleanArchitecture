using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos;
using Onebrb.Core.Interfaces.Services.Messages;
using System;
using System.Collections.Generic;
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

        public async Task<Message> GetMessageById(int id)
        {
            return await _messageRepository.GetAsync(id);
        }
    }
}
