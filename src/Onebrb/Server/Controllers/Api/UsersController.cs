using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUserUserName()
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
