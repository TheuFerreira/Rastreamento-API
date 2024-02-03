using Core.Infra.Models;

namespace Core.Presenters.Responses
{
    public class CourierDeliveriesResponse
    {
        public IEnumerable<DeliveryModel> Deliveries { get; set; }


        public CourierDeliveriesResponse() 
        {
            this.Deliveries = new List<DeliveryModel>();
        }

        public CourierDeliveriesResponse(IEnumerable<DeliveryModel> deliveries) 
        {
            this.Deliveries = deliveries;
        }

    }
}
