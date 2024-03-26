using System.Text.Json.Serialization;

namespace Core.Presenters.Requests
{
    public class NewPositionRequest
    {
        [JsonPropertyName("delivery_id")]
        public int DeliveryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public NewPositionAddressRequest? Address { get; set; }
    }

    public class NewPositionAddressRequest
    {
        public string UF { get; set; }
        public string City { get; set; }

        [JsonPropertyName("cep")]
        public string CEP { get; set; }

        public NewPositionAddressRequest()
        {
            UF = string.Empty;
            City = string.Empty;
            CEP = string.Empty;
        }
    }
}
