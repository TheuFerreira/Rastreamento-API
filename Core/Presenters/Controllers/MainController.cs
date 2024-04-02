using Core.Presenters.Cases;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class MainController : ControllerBase
    {
        private readonly IGetAllSavedDeliveriesCase getAllSavedDeliveriesCase;

        public MainController(IGetAllSavedDeliveriesCase getAllSavedDeliveriesCase)
        {
            this.getAllSavedDeliveriesCase = getAllSavedDeliveriesCase;
        }

        [HttpGet]
        [Route("AllSavedDeliveries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllSavedDeliveriesResponse>))]
        public IActionResult GetAllSavedDeliveries()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            IEnumerable<GetAllSavedDeliveriesResponse> responses = getAllSavedDeliveriesCase.Execute(userId);
            return Ok(responses);
        }
    }
}
