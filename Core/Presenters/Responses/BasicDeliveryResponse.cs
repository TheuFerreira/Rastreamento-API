namespace Core.Presenters.Responses
{
    public class BasicDeliveryResponse
    {
        public string LastUpdate { get; set; }
        public string CreatedDate { get; set; }
        public string Destiny { get; set; }

        public BasicDeliveryResponse() 
        {
            LastUpdate = string.Empty;
            CreatedDate = string.Empty;
            Destiny = string.Empty;
        }

        public BasicDeliveryResponse(string lastUpdate, string createdDate, string destiny)
        {
            LastUpdate=lastUpdate;
            CreatedDate=createdDate;
            Destiny=destiny;
        }
    }
}
