﻿namespace Core.Presenters.Requests
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

        public string Observation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
    }
}

