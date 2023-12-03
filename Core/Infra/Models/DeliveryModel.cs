namespace Core.Infra.Models
{
    public class DeliveryModel
    {
        public int DeliveryId { get; set; }
        public string Observation { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DeliveryCode { get; set; }
        public int? CourierId { get; set; }


        public DeliveryModel() 
        {
            Observation = string.Empty;
            Description = string.Empty;
            Origin = string.Empty;
            Destination = string.Empty;
            DeliveryCode = string.Empty;
        }

        public DeliveryModel(string observation, string description, string deliveryCode, string origin, string destination, int courierId)
        {
            Observation=observation;
            Description=description;
            Origin=origin;
            DeliveryCode= deliveryCode;
            Destination=destination;
            CourierId=courierId;
        }
    }
}
