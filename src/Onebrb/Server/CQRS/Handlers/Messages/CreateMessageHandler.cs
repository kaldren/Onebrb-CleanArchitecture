using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.Messages;
using Onebrb.Server.CQRS.Commands.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Onebrb.Server.CQRS.Handlers.Messages
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, Message>
    {
        private readonly IMessageService<Message> _messageService;

        public CreateMessageHandler(IMessageService<Message> messageService)
        {
            _messageService = messageService;
        }

        public async Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            return await _messageService.CreateMessage(request.Entity);
        }
    }
}
