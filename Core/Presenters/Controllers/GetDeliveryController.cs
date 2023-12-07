using Core.Domain.Exceptions;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class GetDeliveryController : ControllerBase
    {
        private readonly IGetNotSavedDelivery getNotSavedDelivery;

        public GetDeliveryController(IGetNotSavedDelivery getNotSavedDelivery)
        {
            this.getNotSavedDelivery = getNotSavedDelivery;
        }

        [HttpGet]
        [Route("/NotSaved/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasicDeliveryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetNotSavedDelivery(int Id)
        {
            BasicDeliveryResponse response = getNotSavedDelivery.Execute(Id) ?? throw new NotFoundException(); ;

            return Ok(response);
        }



    }

    
}
