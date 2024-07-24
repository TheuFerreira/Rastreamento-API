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
        private readonly ILoginRefreshTokenCase loginRefreshTokenCase;

        public LoginController(ISignInCase signInCase, IResetPasswordCase resetPasswordCase, ILoginRefreshTokenCase loginRefreshTokenCase)
        {
            this.signInCase = signInCase;
            this.resetPasswordCase = resetPasswordCase;
            this.loginRefreshTokenCase = loginRefreshTokenCase;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignInResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            resetPasswordCase.Execute(request.Email);
            return Ok();
        }

        [HttpPost]
        [Route("RefreshToken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginRefreshTokenRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RefreshToken(LoginRefreshTokenRequest request)
        {
            var response = loginRefreshTokenCase.Execute(request);
            return Ok(response);
        }
    }
}
