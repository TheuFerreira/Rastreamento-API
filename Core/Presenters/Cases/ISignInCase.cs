using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface ISignInCase
    {
        SignInResponse Execute(SignInRequest request);
    }
}
