using Core.Domain.Cases;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Produces("application/json")]
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
        public IActionResult GetInfo()
        {
            ClaimsIdentity? identity = (ClaimsIdentity)HttpContext.User.Identity;
            int userId = int.Parse(identity.FindFirst("UserId").Value);

            UserInfoResponse response = getUserInfoCase.Execute(userId);
            return Ok(response);
        }
    }
 
}
