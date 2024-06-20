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

        public int Add(DeliveryModel delivery)
        {
            string sql = @"
                INSERT INTO delivery (id_user, description, address_origin_id, address_destiny_id, observation, code, created_at, last_update_date, status) 
                VALUES (@id_user, @description, @address_origin_id, @address_destiny_id, @observation, @code, @created_at, @last_update_date, @status);
                SELECT last_insert_id();
            ";

            object data = new
            {
                id_user = delivery.CourierId,
                description = delivery.Description,
                address_origin_id = delivery.AddressOriginId,
                address_destiny_id = delivery.AddressDestinyId,
                observation = delivery.Observation,
                code = delivery.Code,
                created_at = delivery.CreatedAt,
                last_update_date = delivery.LastUpdateTime,
                status = delivery.Status
            };

            int lastId = connection.ExecuteScalar<int>(sql, data);
            return lastId;
        }

        public bool UserSavedDelivery(int userId, int deliveryId)
        {
            string sql = @"
                 SELECT COUNT(ud.id_user)
                 FROM user_has_delivery as ud
                 INNER JOIN delivery
                 WHERE ud.id_delivery = @deliveryId AND ud.id_user = @userId AND deleted = 0;
            ";
            object data = new
            {
                deliveryId,
                userId,
            };

            int count = connection.ExecuteScalar<int>(sql, data);
            return count > 0;
        }

        public void AddUser(int deliveryId, int userId)
        {
            DateTime createdAt = DateTime.UtcNow;
            string sql = @"INSERT INTO user_has_delivery (id_delivery, id_user, created_at) VALUES (@deliveryId, @userId, @createdAt);";
            object data = new
            {
                deliveryId,
                userId,
                createdAt
            };

            connection.Execute(sql, data);
        }

        public IEnumerable<DeliveryModel> GetAllUserSaved(int userId)
        {
            string sql = @"
                SELECT d.id_delivery AS DeliveryId, d.description, d.address_origin_id AS AddressOriginId, d.address_destiny_id AS AddressDestinyId, d.observation, d.code, d.last_update_date AS LastUpdateTime, d.status as Status
                FROM delivery AS d
                INNER JOIN user_has_delivery AS ud ON ud.id_delivery = d.id_delivery
                WHERE ud.id_user = @userId AND deleted = 0;
            ";

            object data = new
            {
                userId,
            };

            IEnumerable<DeliveryModel> model = connection.Query<DeliveryModel>(sql, data);
            return model;
        }

        public IEnumerable<DeliveryModel> GetByCode(string code)
        {
            string sql = @"
                SELECT id_delivery AS DeliveryId, description, address_origin_id AS AddressOriginId, address_destiny_id AS AddressDestinyId, observation, code, last_update_date AS LastUpdateTime 
                FROM delivery 
                WHERE BINARY code = @code AND deleted = 0;
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
                SELECT D.created_at AS CreatedAt, D.last_update_date AS LastUpdateTime, D.address_destiny_id AS AddressDestinyId, address_origin_id AS AddressOriginId, D.description AS Description 
                FROM delivery AS D 
                WHERE D.id_delivery = @Id AND deleted = 0;
                    
            ";

            object data = new
            {
                Id,
            };

            DeliveryModel? model = connection.Query<DeliveryModel>(sql, data).FirstOrDefault();
            return model;

        }

        public IEnumerable<DeliveryModel> GetDeliveriesByUserId(int UserId)
        {
            string sql =
            @"
                SELECT D.id_delivery AS DeliveryId, U.id_user AS CourierId, D.description, address_origin_id AS AddressOriginId, address_destiny_id AS AddressDestinyId, D.code, D.observation, D.status, D.last_update_date AS LastUpdateTime, D.created_at AS CreatedAt
                FROM delivery AS D
                JOIN user_has_delivery AS UD on UD.id_delivery = D.id_delivery
                JOIN users AS U on UD.id_user = U.id_user
                WHERE BINARY U.id_user = @UserId AND deleted = 0;
            ";
            object data = new
            {
                UserId,
            };

            IEnumerable<DeliveryModel> model = connection.Query<DeliveryModel>(sql, data);
            return model;

        }

        public DeliveryModel? GetDeliveryByClientId(int DeliveryId, int ClientId)
        {
            string sql =
            @"
                SELECT D.created_at AS CreatedAt, D.last_update_date AS LastUpdateTime, address_origin_id AS AddressOriginId, address_destiny_id AS AddressDestinyId, D.status AS STATUS
                FROM delivery AS D 
                INNER JOIN user_has_delivery AS UD on UD.id_delivery = D.id_delivery
                WHERE UD.id_delivery = @DeliveryId AND UD.id_user = @ClientId AND deleted = 0;
                    
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

        public void UpdateLastUpdateTime(int deliveryId, DateTime currentTime)
        {
            string sql = @"
                UPDATE delivery 
                SET last_update_date = @currentTime 
                WHERE id_delivery = @deliveryId AND deleted = 0;
            ";
            object data = new
            {
                currentTime,
                deliveryId
            };

            connection.Execute(sql, data);
        }

        public void UpdateStatus(int deliveryId, int status, DateTime currentTime)
        {
            string sql = @"
                UPDATE delivery 
                SET status = @status, last_update_date = @currentTime 
                WHERE id_delivery = @deliveryId AND deleted = 0;
            ";
            object data = new
            {
                status,
                currentTime,
                deliveryId
            };

            connection.Execute(sql, data);
        }

        public IEnumerable<DeliveryModel> GetAllOfUser(int userId)
        {
            string sql = @"
                SELECT id_delivery AS DeliveryId, description, address_origin_id AS AddressOriginId, address_destiny_id AS AddressDestinyId, observation, code, last_update_date AS LastUpdateTime, status 
                FROM delivery 
                WHERE BINARY id_user = @userId AND deleted = 0;
            ";
            object data = new
            {
                userId,
            };

            IEnumerable<DeliveryModel> model = connection.Query<DeliveryModel>(sql, data);
            return model;
        }

        public void RemoveFromSaved(int deliveryId, int userId)
        {
            string sql = "DELETE FROM user_has_delivery WHERE id_delivery = @deliveryId AND id_user = @userId AND deleted = 0;";
            object data = new
            {
                deliveryId,
                userId,
            };

            connection.Execute(sql, data);
        }

        public int Update(DeliveryModel delivery)
        {
            string sql = @"
                UPDATE delivery 
                SET description = @description, observation = @observation
                WHERE id_delivery = @deliveryId AND deleted = 0;
            ";

            object data = new
            {
                deliveryId = delivery.DeliveryId,
                description = delivery.Description,
                observation = delivery.Observation,
                last_update_date = delivery.LastUpdateTime
            };

            int lastId = connection.ExecuteScalar<int>(sql, data);
            return lastId;
        }

        public void Delete(int deliveryId)
        {
            string sql = @"
                UPDATE delivery 
                SET deleted = 1
                WHERE id_delivery = @deliveryId;
            ";

            object data = new
            {
                deliveryId
            };

            connection.Execute(sql, data);
        }
    }
}
