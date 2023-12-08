namespace Core.Presenters.Responses
{
    public class BasicDeliveryResponse
    {
        public DateTime LastUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Destiny { get; set; }

        public BasicDeliveryResponse() 
        {
            Destiny = string.Empty;
        }

        public BasicDeliveryResponse(DateTime lastUpdate, string createdDate, string destiny)
        {
            LastUpdate=lastUpdate;
            Destiny=destiny;
        }
    }
}
