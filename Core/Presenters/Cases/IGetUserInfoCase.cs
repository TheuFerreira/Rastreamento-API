using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetUserInfoCase
    {
        UserInfoResponse Execute(int userId);
    }
}
