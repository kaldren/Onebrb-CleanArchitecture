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
    public class GetSentMessagesHandler : IRequestHandler<GetSentMessagesQuery, List<Message>>
    {
        private readonly IMessageService<Message> _messageService;

        public GetSentMessagesHandler(IMessageService<Message> messageService)
        {
            _messageService = messageService;
        }

        public async Task<List<Message>> Handle(GetSentMessagesQuery request, CancellationToken cancellationToken)
        {
            var result = await _messageService.GetAllSentMessages(request.UserId);

            return result.ToList();
        }
    }
}
