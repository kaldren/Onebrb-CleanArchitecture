using MediatR;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos;
using Onebrb.Core.Interfaces.Services.Messages;
using Onebrb.Server.CQRS.Queries.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Onebrb.Server.CQRS.Handlers.Messages
{
    public class GetMessageByIdHandler : IRequestHandler<GetMessageByIdQuery, Message>
    {
        private readonly IMessageService<Message> _messageService;

        public GetMessageByIdHandler(IMessageService<Message> messageRepository)
        {
            _messageService = messageRepository;
        }

        public async Task<Message> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            return await _messageService.GetMessageById(request.Id);
        }
    }
}
