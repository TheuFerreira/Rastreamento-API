using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface ISignUpCase
    { 
        SignUpResponse Execute(SignUpRequest request);
    }
}
