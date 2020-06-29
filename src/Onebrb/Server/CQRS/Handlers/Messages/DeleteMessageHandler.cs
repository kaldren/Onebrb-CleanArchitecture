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
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, Message>
    {
        private readonly IMessageService<Message> _messageService;

        public DeleteMessageHandler(IMessageService<Message> messageService)
        {
            _messageService = messageService;
        }

        public async Task<Message> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            return await _messageService.DeleteMessage(request.Id, request.UserId);
        }
    }
}
