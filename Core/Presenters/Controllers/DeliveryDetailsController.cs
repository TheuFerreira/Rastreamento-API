using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class DeliveryDetailsController : ControllerBase
    {
        private readonly IUpdateDeliveryStatusCase _updateDeliveryStatusCase;
        private readonly IDeleteFromSavedCase _deleteFromSavedCase;

        public DeliveryDetailsController(IUpdateDeliveryStatusCase updateDeliveryStatusCase, IDeleteFromSavedCase deleteFromSavedCase)
        {
            _updateDeliveryStatusCase = updateDeliveryStatusCase;
            _deleteFromSavedCase = deleteFromSavedCase;
        }

        [HttpPut]
        [Route("Status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PutStatus(UpdateDeliveryStatusRequest request)
        {
            _updateDeliveryStatusCase.Execute(request);
            return NoContent();
        }

        [HttpDelete]
        [Route("FromSaved/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteFromSaved(int id)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            _deleteFromSavedCase.Execute(id, userId);

            return NoContent();
        }
    }
}
