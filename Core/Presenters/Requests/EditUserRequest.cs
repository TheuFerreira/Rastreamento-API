namespace Core.Presenters.Requests
{
    public class EditUserRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
