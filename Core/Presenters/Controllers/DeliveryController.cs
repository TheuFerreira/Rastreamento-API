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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class DeliveryController : ControllerBase
    {
        private readonly IGetUserDeliveriesCase _getUserDeliveriesCase;
        private readonly IUpdateDeliveryCase _updateDeliveryCase;
        private readonly IDeleteDeliveryCase _deleteDeliveryCase;
        private readonly IGetDeliveryByIdCase _getDeliveryByIdCase;

        public DeliveryController(IGetUserDeliveriesCase getUserDeliveriesCase, IUpdateDeliveryCase updateDeliveryCase, IDeleteDeliveryCase deleteDeliveryCase, IGetDeliveryByIdCase getDeliveryByIdCase)
        {
            _getUserDeliveriesCase = getUserDeliveriesCase;
            _updateDeliveryCase = updateDeliveryCase;
            _deleteDeliveryCase = deleteDeliveryCase;
            _getDeliveryByIdCase = getDeliveryByIdCase;
        }

        [HttpGet("UserDeliveries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetUserDeliveriesResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserDeliveries()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            IEnumerable<GetUserDeliveriesResponse> responses = _getUserDeliveriesCase.Execute(userId);
            return Ok(responses);
        }

        [HttpPut("Delivery")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PutDelivery(UpdateDeliveryRequest request)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            _updateDeliveryCase.Execute(request, userId);
            return NoContent();
        }

        [HttpDelete("Delivery/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteDelivery(int id)
        {
            _deleteDeliveryCase.Execute(id);
            return NoContent();
        }

        [HttpGet("Delivery/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeliveryInfoResponse))]
        public IActionResult GetDeliveryById(int id)
        {
            DeliveryInfoResponse res = _getDeliveryByIdCase.Execute(id);
            return Ok(res);
        }
    }
}
