using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
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

    public interface IGetDeliveryDetailsByIdCase
    {
        GetDeliveryDetailsByIdResponse Execute(int id);
    }

    public class GetDeliveryDetailsByIdCase : IGetDeliveryDetailsByIdCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IPositionDeliveryRepository positionDeliveryRepository;

        public GetDeliveryDetailsByIdCase(IDeliveryRepository deliveryRepository, IAddressRepository addressRepository, IPositionDeliveryRepository positionDeliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.addressRepository = addressRepository;
            this.positionDeliveryRepository = positionDeliveryRepository;
        }

        public GetDeliveryDetailsByIdResponse Execute(int id)
        {
            DeliveryModel delivery = deliveryRepository.GetById(id) ?? throw new NotFoundException();
            AddressModel address = addressRepository.GetById(delivery.AddressOriginId) ?? throw new NotFoundException();
            PositionDeliveryModel? position = positionDeliveryRepository.GetMostRecentByDelivery(id);

            GetPositionDeliveryResponse? positionResponse = null;
            if (position != null)
            {
                positionResponse = new()
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude,
                };
            }

            GetCEPDeliveryResponse cepResponse = new()
            {
                CEP = address.CEP,
                City = address.City,
                UF = address.UF,
            };

            GetDeliveryDetailsByIdResponse response = new()
            {
                Description = delivery.Description,
                CreatedAt = delivery.CreatedAt,
                LastUpdateTime = delivery.LastUpdateTime,
                Address = cepResponse,
                LastPosition = positionResponse,
            };

            return response;
        }
    }

    public class GetDeliveryDetailsByIdResponse
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public GetCEPDeliveryResponse Address { get; set; }
        public GetPositionDeliveryResponse? LastPosition { get; set; }
    }

    public class GetCEPDeliveryResponse
    {
        public string UF { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
    }

    public class GetPositionDeliveryResponse
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
