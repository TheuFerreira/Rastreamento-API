namespace Core.Infra.Models
{
    public class UserModel
    {
        public UserModel()
        { 
        }
        public UserModel(string fullName, string email, string password, DateOnly birthDate)
        {
            FullName=fullName;
            Email=email;
            Password=password;
            BirthDate=birthDate;
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }

        public string getBirthDateInDateTimeFormat()
        {
            return this.BirthDate.ToString("yyyy-MM-dd") + " 00:00:00";
        }
    }
}
