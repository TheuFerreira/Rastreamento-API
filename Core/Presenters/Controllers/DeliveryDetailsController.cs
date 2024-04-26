using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public DeliveryDetailsController(IUpdateDeliveryStatusCase updateDeliveryStatusCase)
        {
            _updateDeliveryStatusCase = updateDeliveryStatusCase;
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
    }
}
