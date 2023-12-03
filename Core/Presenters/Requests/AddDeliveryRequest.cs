namespace Core.Presenters.Requests
{
    public class AddDeliveryRequest
    {
        public AddDeliveryRequest()
        {
            Observation = string.Empty;
            Description = string.Empty;
            Origin = string.Empty;
            Destination = string.Empty;
        }

        public string Observation { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}

