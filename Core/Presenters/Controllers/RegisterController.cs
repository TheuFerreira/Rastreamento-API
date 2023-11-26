using Core.Domain.Cases;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class RegisterController : ControllerBase
    {
        private readonly ISignUpCase signUpCase;
        
        public RegisterController(ISignUpCase signUpCase)
        {
            this.signUpCase = signUpCase;
        }

        [HttpPost]
        [Route("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SignUp(SignUpRequest request)
        {
            SignUpResponse response = signUpCase.Execute(request);
            return Ok(response);
        }
    }
 
}
