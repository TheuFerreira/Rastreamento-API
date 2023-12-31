﻿using Core.Domain.Exceptions;
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class GetDeliveryController : ControllerBase
    {
        private readonly IGetNotSavedDeliveryCase getNotSavedDeliveryCase;
        private readonly IGetSavedDeliveryCase getSavedDeliveryCase;

        public GetDeliveryController(IGetNotSavedDeliveryCase getNotSavedDeliveryCase, IGetSavedDeliveryCase getSavedDeliveryCase)
        {
            this.getNotSavedDeliveryCase = getNotSavedDeliveryCase;
            this.getSavedDeliveryCase = getSavedDeliveryCase;
        }

        [HttpGet]
        [Route("/NotSaved/{DeliveryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasicDeliveryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetNotSavedDelivery(int DeliveryId)
        {
            BasicDeliveryResponse response = getNotSavedDeliveryCase.Execute(DeliveryId) ?? throw new NotFoundException(); ;

            return Ok(response);
        }


        [HttpGet]
        [Route("/Saved/{DeliveryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasicDeliveryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSavedDelivery(int DeliveryId) 
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new InvalidCredentialException();
            Claim claimUserId = identity.FindFirst("UserId") ?? throw new InvalidCredentialException();
            int ClientId = int.Parse(claimUserId.Value);
            BasicDeliveryResponse response = getSavedDeliveryCase.Execute(DeliveryId, ClientId);

            return Ok(response);
        }



    }


}
