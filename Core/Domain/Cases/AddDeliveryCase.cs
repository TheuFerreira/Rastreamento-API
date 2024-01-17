using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;

namespace Core.Domain.Cases
{
    public class AddDeliveryCase : IAddDeliveryCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IUserRepository userRepository;
        public AddDeliveryCase(IDeliveryRepository deliveryRepository, IUserRepository userRepository) 
        {
            this.deliveryRepository = deliveryRepository;
            this.userRepository = userRepository;
        }

        public void Execute(DeliveryModel model)
        {

            if(model == null) throw new ArgumentNullException("model");
            if (
                model.Destination == null
                || model.Observation == null
                //|| model.CostumerId == null Verificar se o cliente vai ser definido na hora de cadastrar o pacote
                || model.CourierId == null
                || model.Description == null
                || model.Origin == null)
                throw new BadHttpRequestException("Preencha todos os campos disponíveis!");

            if (userRepository.GetById((int)model.CourierId) == null) throw new NotFoundException();

            model.Status = "Aguardando Coleta";
            model.Code = Guid.NewGuid().ToString().ToString();

            this.deliveryRepository.Add(model);
            
        }
    }
}
