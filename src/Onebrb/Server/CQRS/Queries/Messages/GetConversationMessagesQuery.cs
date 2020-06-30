using MediatR;
using Onebrb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.CQRS.Queries.Messages
{
    public class GetConversationMessagesQuery : IRequest<List<Message>>
    {
        public int CurrentUserId { get; set; }
        public int OtherUserId { get; set; }

        public GetConversationMessagesQuery(int currentUserId, int otherUserId)
        {
            CurrentUserId = currentUserId;
            OtherUserId = otherUserId;
        }
    }
}
