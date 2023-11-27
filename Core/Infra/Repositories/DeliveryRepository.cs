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
            string sql = @"";

            object data = new
            {

            };

            connection.Execute(sql, data);
            
        }
    }
}
