using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.Messages;
using Onebrb.Server.Controllers.Api;
using Onebrb.Server.CQRS.Commands.Messages;
using Onebrb.Server.CQRS.Handlers.Messages;
using Onebrb.Server.CQRS.Queries.Messages;
using Onebrb.Server.Utils.Http;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Onebrb.UnitTests.Controllers
{
    public class TestMessagesController
    {
        [Fact]
        public async Task GetMessageById_MessageIdExists_ReturnMessage()
        {
            // Arrange
            int messageId = 1;
            int expectedId = 1;
            var mockService = new Mock<IMessageService<Message>>();
            var expectedMessage = new Message
            {
                Id = messageId,
                AuthorId = 1,
                AuthorUserName = "kaldren",
                Body = "Test body",
                DateSent = DateTime.UtcNow,
                IsArchivedForAuthor = false,
                IsArchivedForRecipient = false,
                IsDeletedForAuthor = false,
                IsDeletedForRecipient = false,
                RecipientId = 2,
                RecipientUserName = "johndoe",
            };
            mockService.Setup(service => service.GetMessageById(messageId)).ReturnsAsync(expectedMessage);
            var handler = new GetMessageByIdHandler(mockService.Object);

            // Act
            var actualResult = await handler.Handle(new GetMessageByIdQuery(messageId), CancellationToken.None);

            // Assert 
            Assert.IsType<Message>(actualResult);
            Assert.Equal(expectedId, actualResult.Id);
        }

        [Fact]
        public async Task GetMessageById_MessageIdDoesNotExist_ReturnNotFound()
        {
            // Arrange
            int messageId = -999;
            var mockMediator = new Mock<IMediator>();
            var controller = new MessagesController(mockMediator.Object, null, null, null, null);
            int expectedStatusCode = (int)HttpStatusCode.NotFound;

            // Act
            var response = await controller.Get(messageId);

            //Assert
            var result = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public async Task DeleteMessageById_MessageIdExists_DeleteMessage()
        {
            // Arrange
            int messageId = 1;
            int userId = 1;
            int expectedId = 1;
            var mockService = new Mock<IMessageService<Message>>();
            var expectedMessage = new Message
            {
                Id = messageId,
                AuthorId = 1,
                AuthorUserName = "kaldren",
                Body = "Test body",
                DateSent = DateTime.UtcNow,
                IsArchivedForAuthor = false,
                IsArchivedForRecipient = false,
                IsDeletedForAuthor = false,
                IsDeletedForRecipient = false,
                RecipientId = 2,
                RecipientUserName = "johndoe",
            };
            mockService.Setup(service => service.DeleteMessage(messageId, userId)).ReturnsAsync(expectedMessage);
            var handler = new DeleteMessageHandler(mockService.Object);

            // Act
            var actualResult = await handler.Handle(new DeleteMessageCommand(messageId, userId), CancellationToken.None);

            // Assert 
            Assert.IsType<Message>(actualResult);
            Assert.Equal(expectedId, actualResult.Id);
        }
    }
}
