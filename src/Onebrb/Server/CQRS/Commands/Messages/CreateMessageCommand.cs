using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Dtos.Messages;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.Messages;

namespace Onebrb.Server.CQRS.Commands.Messages
{
    public class CreateMessageCommand : IRequest<Message>
    {
        public Message Entity { get; set; }

        public CreateMessageCommand(Message entity)
        {
            Entity = entity;
        }
    }
}
