using MediatR;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.Messages;
using Onebrb.Server.CQRS.Queries.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Onebrb.Server.CQRS.Handlers.Messages
{
    public class GetConversationMessagesHandler : IRequestHandler<GetConversationMessagesQuery, List<Message>>
    {
        private readonly IMessageService<Message> _messageService;

        public GetConversationMessagesHandler(IMessageService<Message> messageService)
        {
            _messageService = messageService;
        }

        public async Task<List<Message>> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
        {
            var result = await _messageService.GetConversationWithUser(request.CurrentUserId, request.OtherUserId);

            return result.ToList();
        }
    }
}
