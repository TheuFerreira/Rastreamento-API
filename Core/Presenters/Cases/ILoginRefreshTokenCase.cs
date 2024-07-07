using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface ILoginRefreshTokenCase
    {
        SignInResponse Execute(LoginRefreshTokenRequest request);
    }
}
