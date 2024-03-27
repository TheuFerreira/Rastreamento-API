using Core.Infra.Models;

namespace Core.Presenters.Responses
{
    public class CourierDeliveriesResponse
    {
        public IEnumerable<GetSavedDeliveriesResponse> Deliveries { get; set; }


        public CourierDeliveriesResponse() 
        {
            this.Deliveries = new List<GetSavedDeliveriesResponse>();
        }

        public CourierDeliveriesResponse(IEnumerable<GetSavedDeliveriesResponse> deliveries) 
        {
            this.Deliveries = deliveries;
        }

    }
}
