using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Mvc;

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
            addDeliveryCase.Execute(new Infra.Models.DeliveryModel(request.Observation, request.Description, request.Origin, request.DeliveryCode, request.Destination, request.CourierId));
            return Ok();
        }
    }
}

