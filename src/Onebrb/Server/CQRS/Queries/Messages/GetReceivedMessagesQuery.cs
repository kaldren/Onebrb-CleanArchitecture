using MediatR;
using Onebrb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.CQRS.Queries.Messages
{
    public class GetReceivedMessagesQuery : IRequest<List<Message>>
    {
        public int UserId { get; set; }
        public GetReceivedMessagesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
