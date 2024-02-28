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

        public void Insert(PositionDeliveryModel positionDelivery)
        {
            string sql = "INSERT INTO delivery_position (id_delivery, latitude, longitude, created_at) VALUES (@id_delivery, @latitude, @longitude, @created_at);";
            object data = new
            {
                @id_delivery = positionDelivery.DeliveryId,
                @latitude = positionDelivery.Latitude,
                @longitude = positionDelivery.Longitude,
                @created_at = positionDelivery.CreatedAt
            };

            connection.ExecuteScalar(sql, data);
        }
    }
}
