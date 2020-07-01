using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onebrb.Core.Dtos.User;
using Onebrb.Core.Entities;
using Onebrb.Server.Utils.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Onebrb.Server.Controllers.Api
{
    /// <summary>
    /// This controller handles requests to api/users
    /// </summary>
    [Produces("application/json")]
    public class UsersController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <response code="200">Returns the requested user</response>
        /// <response code="404">The user was not found</response>
        /// <returns>The requested user or null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return NotFound(new HttpResponseHandler
                {
                    Message = $"User not found.",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }

            var dto = _mapper.Map<UserDto>(user);

            return Ok(dto);
        }

        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <response code="200">Returns the requested user</response>
        /// <response code="404">The user was not found</response>
        /// <returns>The requested user or null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound(new HttpResponseHandler
                {
                    Message = $"User not found.",
                    StatusCode = HttpStatusCode.NotFound,
                });
            }

            var dto = _mapper.Map<UserDto>(user);

            return Ok(dto);
        }


        /// <summary>
        /// Gets the currently logged in user
        /// </summary>
        /// <returns>The currently logged in user or null</returns>
        [HttpGet("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return BadRequest(new HttpResponseHandler
                {
                    Message = $"You are not logged in.",
                    StatusCode = HttpStatusCode.BadRequest,
                });
            }

            var userDto = _mapper.Map<UserDto>(currentUser);

            return Ok(userDto);
        }
    }
}
