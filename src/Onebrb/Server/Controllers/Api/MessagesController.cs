﻿#define TESTING
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Onebrb.Core.Dtos.Messages;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Services.User;
using Onebrb.Server.CQRS.Commands.Messages;
using Onebrb.Server.CQRS.Queries.Messages;
using Onebrb.Server.Utils.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Onebrb.Server.Controllers.Api
{
    /// <summary>
    /// This controller handles requests to api/messages
    /// </summary>
    [Produces("application/json")]
    //[Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MessagesController> _logger;
        private readonly IUserService<ApplicationUser> _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(IMediator mediator, 
            ILogger<MessagesController> logger, 
            UserManager<ApplicationUser> userManager, 
            IUserService<ApplicationUser> userService,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a message by id
        /// </summary>
        /// <remarks>
        /// Example: GET api/messages/5    
        /// </remarks>
        /// <param name="id">The message id</param>
        /// <returns>The message</returns>
        /// <response code="200">Returns the requested message</response>
        /// <response code="400">The requested is not valid</response>
        /// <response code="404">The requested message is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetMessageByIdQuery(id));

            if (result == null)
            {
#if !TESTING
                _logger.LogWarning($"Message with id {id} doesn't exist.");
#endif
                return NotFound(new HttpResponseHandler
                {
                    Message = $"Message with id {id} doesn't exist.",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets all received, sent or archived messages
        /// </summary>
        /// <param name="type">Messages type (sent, received or archived)</param>
        /// <param name="with">The user with whom we have a conversation</param>
        /// <response code="200">Returns the requested messages</response>
        /// <response code="200">The messages weren't found</response>
        /// <returns>All of the user's sent, received or archived messages</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] string type = "received", int with = -1)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            List<Message> messages = new List<Message>();

            switch (type)
            {
                case "sent":
                    messages = await _mediator.Send(new GetSentMessagesQuery(currentUser.Id));
                    break;
                case "received":
                    messages = await _mediator.Send(new GetReceivedMessagesQuery(currentUser.Id));
                    break;
                case "archived":
                    messages = await _mediator.Send(new GetArchivedMessagesQuery(currentUser.Id));
                    break;
                case "conversation":
                    messages = await _mediator.Send(new GetConversationMessagesQuery(currentUser.Id, with));
                    break;
                default:
                    messages = await _mediator.Send(new GetReceivedMessagesQuery(currentUser.Id));
                    break;
            }

            var viewModel = _mapper.Map<List<MessageDto>>(messages);

            return Ok(viewModel);
        }

        /// <summary>
        /// Creates a new message
        /// </summary>
        /// <param name="entity">The Message entity</param>
        /// <returns>The newly created message</returns>
        /// <response code="201">Returns the created message</response>
        /// <response code="400">The request is not valid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Message entity)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var recipient = await _userService.GetUserByUserNameAsync(entity.RecipientUserName);

            if (currentUser == recipient)
            {
                return BadRequest(new HttpResponseHandler
                {
                    Message = $"You can't send messages to yourself.",
                    StatusCode = HttpStatusCode.BadRequest
                }); ;
            }

            if (recipient == null)
            {
                return BadRequest(new HttpResponseHandler
                {
                    Message = $"The {nameof(recipient)} cannot be found.",
                    StatusCode = HttpStatusCode.BadRequest
                }); ;
            }

            entity.AuthorId = currentUser.Id;
            entity.AuthorUserName = currentUser.UserName;
            entity.DateSent = DateTime.UtcNow;

            var result = await _mediator.Send(new CreateMessageCommand(entity));

            if (result == null)
            {
                _logger.LogWarning($"Message from {entity.AuthorUserName} to {entity.RecipientUserName} failed.");
                return BadRequest(new HttpResponseHandler
                {
                    Message = $"Failed to send the message.",
                    StatusCode = HttpStatusCode.BadRequest,
                });
            }

            return Ok(result);
        }


        /// <summary>
        /// Deletes a message
        /// </summary>
        /// <param name="id">The message id</param>
        /// <returns>The deleted message</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _mediator.Send(new DeleteMessageCommand(id, currentUser.Id));

            if (result == null)
            {
                _logger.LogWarning($"Unable to delete message with id {id}.");

                return BadRequest(new HttpResponseHandler
                {
                    Message = $"Unable to delete message with id {id}.",
                    StatusCode = HttpStatusCode.BadRequest,
                });
            }

            return Ok(result);
        }
    }
}
