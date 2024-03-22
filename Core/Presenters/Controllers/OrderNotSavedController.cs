using Core.Presenters.Cases;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class OrderNotSavedController : ControllerBase
    {
        private readonly IGetDeliveryDetailsByIdCase _getDeliveryDetailsByIdCase;

        public OrderNotSavedController(IGetDeliveryDetailsByIdCase getDeliveryDetailsByIdCase)
        {
            _getDeliveryDetailsByIdCase = getDeliveryDetailsByIdCase;
        }

        [HttpGet]
        [Route("Details/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDeliveryDetailsByIdResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDetails(int id)
        {
            GetDeliveryDetailsByIdResponse response = _getDeliveryDetailsByIdCase.Execute(id);
            return Ok(response);
        }
    }
}
