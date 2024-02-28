﻿using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Presenters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PositionController : ControllerBase
    {
        private readonly IAddNewPositionCase addNewPositionCase;

        public PositionController(IAddNewPositionCase addNewPositionCase)
        {
            this.addNewPositionCase = addNewPositionCase;
        }

        [HttpPost]
        [Route("NewPosition")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult NewPosition(NewPositionRequest request)
        {
            addNewPositionCase.Execute(request);
            return Ok();
        }
    }
}
