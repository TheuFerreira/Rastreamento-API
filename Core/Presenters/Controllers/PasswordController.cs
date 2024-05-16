using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public class PasswordController : Controller
    {

        private readonly IChangePasswordCase changePasswordCase;

        public PasswordController(IChangePasswordCase changePasswordCase)
        {
            this.changePasswordCase = changePasswordCase;
        }


        [HttpPatch]
        [Route("Change")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangePasswordResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChangeUserPassword(ChangePasswordRequest changePasswordRequest) 
        {

            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            ChangePasswordResponse response =  changePasswordCase.Execute(changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword, userId);

            if(response.status)
                return Ok(response);
            else
                return BadRequest(response);
        }

    }
}
