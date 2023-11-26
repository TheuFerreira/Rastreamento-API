using Core.Domain.Cases;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Authentication;
using System.Security.Claims;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class EditController : ControllerBase
    {
        private readonly IEditUserCase editUserCase;
        private readonly IGetUserInfoCase getUserInfoCase;
        
        public EditController(IDbConnection connection, IGetUserInfoCase getUserInfoCase)
        {
            IUserRepository userRepository = new UserRepository(connection);
             editUserCase = new EditUserCase(userRepository);
            this.getUserInfoCase = getUserInfoCase;
        }

        [HttpPut]
        [Route("EditUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditUser(EditUserRequest request)
        {
             SignUpResponse response = editUserCase.Execute(request);
             return Ok(response);
           // return Ok(new SignUpResponse());
        }

        [HttpGet]
        [Route("Info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfoResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetInfo()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            UserInfoResponse response = getUserInfoCase.Execute(userId);
            return Ok(response);
        }
    }
 
}
