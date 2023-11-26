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
                SELECT id_user AS UserId
                FROM users 
                WHERE BINARY email = @email 
                    AND BINARY password = MD5(@password);
            ";
            object data = new
            {
                email,
                password
            };

            UserModel? userModel = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return userModel;
        }

        public UserModel? GetByEmail(string email)
        {
            string sql = @"
            SELECT id_user AS UserId
            FROM users
            WHERE BINARY email = @email;
            ";

            object data = new { email };

            UserModel? userModel = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return userModel;
        }

        public void Add(UserModel user)
        {

            string sql = @"
            INSERT INTO 
            users (name, email, password, birth_date, created_at, updated_at) 
            VALUES 
            (@name, @email, MD5(@password), @birth_date, @created_at, @updated_at);
            ";

            object data = new { name = user.FullName, email = user.Email, password = user.Password, birth_date = user.getBirthDateInDateTimeFormat(), created_at = DateTime.Now, updated_at = DateTime.Now };

            connection.Query<UserModel>(sql, data);
        }
    }
}
