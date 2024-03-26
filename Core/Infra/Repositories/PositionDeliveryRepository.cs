using Core.Domain.Repositories;
using Core.Infra.Models;
using Dapper;
using System.Data;

namespace Core.Infra.Repositories
{
    public class PositionDeliveryRepository : IPositionDeliveryRepository
    {
        private readonly IDbConnection connection;

        public PositionDeliveryRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public PositionDeliveryModel? GetMostRecentByDelivery(int deliveryId)
        {
            string sql = @"
                SELECT id_address AS AddressId, id_delivery AS DeliveryId, latitude, longitude, created_at AS CreatedAt 
                FROM delivery_position 
                WHERE id_delivery = @id
                ORDER BY id_delivery_position DESC 
                LIMIT 1;
            ";
            object data = new
            {
                id = deliveryId
            };

            PositionDeliveryModel? model = connection.Query<PositionDeliveryModel>(sql, data).FirstOrDefault();
            return model;
        }

        public void Insert(PositionDeliveryModel positionDelivery)
        {
            string sql = @"
                INSERT INTO delivery_position (id_address, id_delivery, latitude, longitude, created_at) 
                VALUES (@id_address, @id_delivery, @latitude, @longitude, @created_at);
            ";
            object data = new
            {
                @id_address = positionDelivery.AddressId,
                @id_delivery = positionDelivery.DeliveryId,
                @latitude = positionDelivery.Latitude,
                @longitude = positionDelivery.Longitude,
                @created_at = positionDelivery.CreatedAt
            };

            connection.ExecuteScalar(sql, data);
        }
    }
}
