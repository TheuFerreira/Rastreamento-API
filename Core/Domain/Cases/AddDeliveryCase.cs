using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Domain.Services;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class AddDeliveryCase : IAddDeliveryCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IUserRepository userRepository;
        private readonly IGenerateDeliveryCodeService generateDeliveryCodeService;

        public AddDeliveryCase(IDeliveryRepository deliveryRepository, IUserRepository userRepository, IGenerateDeliveryCodeService generateDeliveryCodeService)
        {
            this.deliveryRepository = deliveryRepository;
            this.userRepository = userRepository;
            this.generateDeliveryCodeService = generateDeliveryCodeService;
        }

        public AddDeliveryResponse Execute(DeliveryModel model)
        {
            if (model == null) throw new ArgumentNullException();

            if (
                model.Destination == null
                || model.Observation == null
                //TODO: || model.CostumerId == null Verificar se o cliente vai ser definido na hora de cadastrar o pacote
                || model.CourierId == null
                || model.Description == null
                || model.Origin == null)
                throw new BadHttpRequestException("Preencha todos os campos disponíveis!");

            if (userRepository.GetById((int)model.CourierId) == null) throw new NotFoundException();

            model.Status = "Aguardando Coleta";
            model.Code = generateDeliveryCodeService.GenerateDeliveryCode();

            int id = this.deliveryRepository.Add(model);

            return new AddDeliveryResponse
            {
                Id = id,
                Code = model.Code
            };
        }
    }
}
