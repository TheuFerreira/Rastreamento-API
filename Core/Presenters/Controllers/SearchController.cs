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
    public class SearchController : ControllerBase
    {
        private readonly ISearchDeliveryCase searchOrderCase;

        public SearchController(ISearchDeliveryCase searchDeliveryCase)
        {
            searchOrderCase = searchDeliveryCase;
        }

        [HttpPost]
        [Route("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostSearch(SearchRequest request)
        {
            IEnumerable<SearchResponse> responses = searchOrderCase.Execute(request);
            return Ok(responses);
        }
    }
}
