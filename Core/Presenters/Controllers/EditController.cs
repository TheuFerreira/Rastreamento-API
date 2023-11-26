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
    public class EditController : ControllerBase
    {
        private readonly IEditUserCase editUserCase;
        
        public EditController(IDbConnection connection)
        {
            IUserRepository userRepository = new UserRepository(connection);
             editUserCase = new EditUserCase(userRepository);
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
    }
 
}
