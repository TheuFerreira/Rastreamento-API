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

            object data = new 
            { 
                name = user.FullName, 
                email = user.Email, 
                password = user.Password, 
                birth_date = user.GtBirthDateInDateTimeFormat(), 
                created_at = DateTime.UtcNow, 
                updated_at = DateTime.UtcNow 
            };

            connection.Execute(sql, data);
        }

        public void Update(UserModel user)
        {
            string slq = @"
                update users 
                set name = @name, email = @email, birth_date = @birth_date, updated_at = @updated_at 
                where id_user = @id_user;
            ";

            object data = new 
            { 
                id_user = user.UserId, 
                name = user.FullName, 
                email = user.Email, 
                birth_date = user.GtBirthDateInDateTimeFormat(), updated_at = DateTime.Now 
            };

            connection.Query(slq, data);

        }

        public UserModel? GetById(int userId)
        {
            string sql = @"
                SELECT id_user AS UserId, name AS FullName, email, birth_date AS BirthDate 
                FROM users 
                WHERE id_user = @userId;
            ";
            object data = new
            {
                userId
            };

            UserModel? model = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return model;
        }

        public UserModel? GetByIdAndPassword(int userId, string password) 
        {
            string sql = @"
                SELECT users.id_user AS UserId
                FROM users 
                WHERE users.id_user = @userId AND BINARY users.password = MD5(@password);
            ";

            object data = new
            {
                userId,
                password
            };

            UserModel? model = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return model;
        }

        public void SetNewPassword(int userId, string CurrentPassword, string NewPassword)
        {
            string slq = @"
            UPDATE users " +
            "SET password = MD5(@NewPassword)" +
            "WHERE id_user = @userId AND BINARY users.password = MD5(@CurrentPassword);";

            object data = new { userId, NewPassword, CurrentPassword };

            connection.Query(slq, data);
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            string slq = @"
            UPDATE users 
            SET password = MD5(@NewPassword)
            WHERE id_user = @userId;
            ";

            object data = new { userId, newPassword };
            connection.Query(slq, data);
        }
    }
}
