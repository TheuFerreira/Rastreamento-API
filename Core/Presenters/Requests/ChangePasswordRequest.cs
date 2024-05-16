namespace Core.Presenters.Requests
{
    public class ChangePasswordRequest
    {
            public ChangePasswordRequest()
            {
                CurrentPassword = string.Empty;
                NewPassword = string.Empty;
            }

            public string CurrentPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }
}
