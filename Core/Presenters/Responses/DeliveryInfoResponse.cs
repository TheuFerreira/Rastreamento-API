using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class DeliveryInfoResponse
    {
        public int DeliveryId { get; set; }
        public string Observation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DeliveryAddressInfoResponse Origin { get; set; }
        public DeliveryAddressInfoResponse Destiny { get; set; }

        public DeliveryInfoResponse()
        {
            Origin = new DeliveryAddressInfoResponse();
            Destiny = new DeliveryAddressInfoResponse();
        }
    }

    public class DeliveryAddressInfoResponse
    {
        [JsonPropertyName("address_id")]
        public int? AddressId { get; set; }
        public string CEP { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
    }
}
