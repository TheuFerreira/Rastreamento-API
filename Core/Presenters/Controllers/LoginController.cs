using Core.Domain.Cases;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class LoginController : ControllerBase
    {
        private readonly ISignInCase signInCase;

        public LoginController(IDbConnection connection, JWTModel jwtModel)
        {
            IUserRepository userRepository = new UserRepository(connection);
            signInCase = new SignInCase(userRepository, jwtModel);
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
    }
}
