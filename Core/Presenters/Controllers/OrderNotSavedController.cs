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
    [AllowAnonymous]
    public class OrderNotSavedController : ControllerBase
    {
        private readonly IGetDeliveryDetailsByIdCase _getDeliveryDetailsByIdCase;
        private readonly IUserSaveDeliveryCase _userSaveDeliveryCase;

        public OrderNotSavedController(IGetDeliveryDetailsByIdCase getDeliveryDetailsByIdCase, IUserSaveDeliveryCase userSaveDeliveryCase)
        {
            _getDeliveryDetailsByIdCase = getDeliveryDetailsByIdCase;
            _userSaveDeliveryCase = userSaveDeliveryCase;
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

        [HttpPost]
        [Route("Save/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Save(int id)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int userId = int.Parse(claimUserId.Value);

            _userSaveDeliveryCase.Execute(userId, id);
            return NoContent();
        }
    }
}
