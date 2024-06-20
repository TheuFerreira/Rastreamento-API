using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class LoginController : ControllerBase
    {
        private readonly ISignInCase signInCase;
        private readonly IResetPasswordCase resetPasswordCase;

        public LoginController(ISignInCase signInCase, IResetPasswordCase resetPasswordCase)
        {
            this.signInCase = signInCase;
            this.resetPasswordCase = resetPasswordCase;
        }

        [HttpPost]
        [Route("SignIn")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignInResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SignIn(SignInRequest request)
        {
            SignInResponse result = signInCase.Execute(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            resetPasswordCase.Execute(request.Email);
            return Ok();
        }
    }


}
