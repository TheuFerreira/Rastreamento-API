using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class GetDeliveryDetailsByIdResponse
    {
        public string Description { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("last_update_time")]
        public DateTime LastUpdateTime { get; set; }
        public GetCEPDeliveryResponse Address { get; set; }

        [JsonPropertyName("last_position")]
        public GetPositionDeliveryResponse? LastPosition { get; set; }

        public GetDeliveryDetailsByIdResponse()
        {
            Description = string.Empty;
            Address = new GetCEPDeliveryResponse();
        }
    }

    public class GetCEPDeliveryResponse
    {
        public string UF { get; set; }
        public string City { get; set; }

        [JsonPropertyName("cep")]
        public string CEP { get; set; }

        public GetCEPDeliveryResponse()
        {
            UF = string.Empty;
            City = string.Empty;
            CEP = string.Empty;
        }
    }

    public class GetPositionDeliveryResponse
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public GetCEPDeliveryResponse? Address { get; set; }
    }
}
