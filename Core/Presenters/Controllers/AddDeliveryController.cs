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
    public class AddDeliveryController : Controller
    {
        private readonly IAddDeliveryCase addDeliveryCase;

        public AddDeliveryController(IAddDeliveryCase addDeliveryCase)
        {
            this.addDeliveryCase = addDeliveryCase;
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Index(AddDeliveryRequest request)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int courierId = int.Parse(claimUserId.Value);
            addDeliveryCase.Execute(new Infra.Models.DeliveryModel(request.Observation, request.Description, request.Origin, request.Destination, courierId));
            return Ok();
        }
    }
}

