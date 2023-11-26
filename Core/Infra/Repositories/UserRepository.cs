using Core.Domain.Repositories;
using Core.Infra.Models;
using Dapper;
using System.Data;

namespace Core.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection connection;

        public UserRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public UserModel? GetByEmailAndPassword(string email, string password)
        {
            string sql = @"
                SELECT id_user 
                FROM users 
                WHERE BINARY email = @email 
                    AND BINARY password = @password;
            ";
            object data = new
            {
                email,
                password
            };

            UserModel? userModel = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return userModel;
        }
    }
}
