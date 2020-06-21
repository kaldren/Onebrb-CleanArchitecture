using MediatR;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.CQRS.Queries.Messages
{
    public class GetMessageByIdQuery : IRequest<Message>
    {
        public int Id { get; set; }

        public GetMessageByIdQuery(int id)
        {
            Id = id;
        }
    }
}
