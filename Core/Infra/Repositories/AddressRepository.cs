using Core.Domain.Repositories;
using Core.Infra.Models;
using Dapper;
using System.Data;

namespace Core.Infra.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDbConnection connection;

        public AddressRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public int Add(AddressModel model)
        {
            string sql = @"
            INSERT INTO address(cep, uf, city, district, street, number, complement) 
            VALUES (@cep, @uf, @city, @district, @street, @number, @complement);
            SELECT last_insert_id();
            ";
            object data = new
            {
                @cep = model.CEP,
                @uf = model.UF,
                @city = model.City,
                @district = model.District,
                @street = model.Street,
                @number = model.Number,
                @complement = model.Complement
            };

            int id = connection.ExecuteScalar<int>(sql, data);
            return id;
        }
    }
}
