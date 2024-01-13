namespace Core.Infra.Models
{
    public class DeliveryModel
    {
        public int DeliveryId { get; set; }
        public int? CourierId { get; set; }
        public string Observation { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreatedAt { get; set; }


        public DeliveryModel() 
        {
            Observation = string.Empty;
            Description = string.Empty;
            Origin = string.Empty;
            Destination = string.Empty;
            Code = string.Empty;
            Status = string.Empty;
        }

        public DeliveryModel(string observation, string description, string origin, string destination, int courierId)
        {
            Observation=observation;
            Description=description;
            Origin=origin;
            Destination=destination;
            Code=string.Empty;
            CourierId=courierId;
            Status=String.Empty;
        }
    }
}
