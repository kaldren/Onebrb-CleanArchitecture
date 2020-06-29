using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Dtos.Messages;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.Messages;

namespace Onebrb.Server.CQRS.Commands.Messages
{
    public class DeleteMessageCommand : IRequest<Message>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DeleteMessageCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
