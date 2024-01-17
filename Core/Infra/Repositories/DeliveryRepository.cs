using Core.Domain.Repositories;
using Core.Infra.Models;
using Dapper;
using System;
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
            string sql = @"INSERT INTO delivery (id_user, description, origin, destiny, observation, code, created_at, last_update_date, status) VALUES (@id_user, @description, @origin, @destiny, @observation, @code, @created_at, @last_update_date, @status);";
       
            object data = new
            {
                id_user = delivery.CourierId,
                description = delivery.Description,
                origin = delivery.Origin,
                destiny = delivery.Destination,
                observation = delivery.Observation,
                code = delivery.Code,
                created_at = DateTime.UtcNow,
                last_update_date = DateTime.UtcNow,
                status = delivery.Status
            };

            connection.Execute(sql, data);
        }

        public IEnumerable<DeliveryModel> GetByCode(string code)
        {
            string sql = @"
                SELECT id_delivery AS DeliveryId, description, origin, destiny AS Destination, observation, code, last_update_date AS LastUpdateTime 
                FROM delivery 
                WHERE BINARY code = @code;
            ";
            object data = new
            {
                code,
            };

            IEnumerable<DeliveryModel> model = connection.Query<DeliveryModel>(sql, data);
            return model;
        }

        public DeliveryModel? GetById(int Id)
        {
            string sql =
            @"
                SELECT D.created_at AS CreatedAt, D.last_update_date AS UpdatedAt, D.destiny AS Destination 
                FROM delivery AS D 
                WHERE D.id_delivery = @Id AND @Id NOT IN (SELECT UD.id_delivery FROM user_has_delivery as UD);
                    
            ";

            object data = new
            {
                Id,
            };
            connection.Execute(sql, data);

            DeliveryModel? model = connection.Query<DeliveryModel>(sql, data).FirstOrDefault();
            return model;

        }
        public DeliveryModel? GetDeliveryByClientId(int DeliveryId, int ClientId)
        {
            string sql =
            @"
                SELECT D.created_at AS CreatedAt, D.last_update_date AS UpdatedAt, D.destiny AS Destination, D.origin AS Origin, D.status AS STATUS
                FROM delivery AS D 
                INNER JOIN user_has_delivery AS UD on UD.id_delivery = D.id_delivery
                WHERE UD.id_delivery = @DeliveryId AND UD.id_user = @ClientId;
                    
            ";



            object data = new
            {
                ClientId,
                DeliveryId
            };
            connection.Execute(sql, data);

            DeliveryModel? model = connection.Query<DeliveryModel>(sql, data).FirstOrDefault();
            return model;
        }
    }
}
