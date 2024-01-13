namespace Core.Presenters.Responses
{
    public class DetailedDeliveryResponse
    {
        public DateTime LastUpdate { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Origin { get; set; }

        public string CurrentStatus { get; set; }

        public string Destiny { get; set; }

        public DetailedDeliveryResponse()
        {
            Destiny = string.Empty;
            LastUpdate = DateTime.MinValue;
            CreatedDate = DateTime.MinValue;
            Origin = string.Empty;
            CurrentStatus = string.Empty;
        }

        public DetailedDeliveryResponse(DateTime lastUpdate, DateTime createdDate, string origin, string destiny, string status)
        {
            CurrentStatus=status;
            Origin=origin;
            LastUpdate=lastUpdate;
            Destiny=destiny;
            CreatedDate=createdDate;
        }
    }
}

