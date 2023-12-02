using Core.Domain.Repositories;
using Core.Infra.Models;
using Dapper;
using System.Data;

namespace Core.Infra.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IDbConnection connection;
        public DeliveryRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
        public void Add(DeliveryModel delivery)
        {
            string sql = @"INSERT INTO delivery (id_user, description, origin, destiny, observation, code, created_at, last_update_date) VALUES (@id_user, @description, @origin, @destiny, @observation, @code, @created_at, @last_update_date);";
       
            object data = new
            {
                id_user = delivery.CourierId,
                description = delivery.Description,
                origin = delivery.Origin,
                destiny = delivery.Destination,
                observation = delivery.Observation,
                code = delivery.DeliveryCode,
                created_at = DateTime.UtcNow,
                last_update_date = DateTime.UtcNow,

            };

            connection.Execute(sql, data);

        }
    }
}
