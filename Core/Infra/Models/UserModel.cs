namespace Core.Infra.Models
{
    public class UserModel
    {
        public UserModel()
        {
            FullName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        public UserModel(string fullName, string email, string password, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            BirthDate = birthDate;
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }

        public string GetDateFromBirthDate()
        {
            return BirthDate.ToString("yyyy-MM-dd");
        }

        public string GtBirthDateInDateTimeFormat()
        {
            return this.BirthDate.ToString("yyyy-MM-dd") + " 00:00:00";
        }
    }
}
