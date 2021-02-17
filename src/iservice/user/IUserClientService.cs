using foundation.config;
using foundation.ef5.poco;
using irespository.user.client.model;

namespace iservice.user
{
    public interface IUserClientService
    {
        PagerResult<UserClientListApiModel> GetPagerList(PagerQuery<UserClientListQueryModel> query);
        UserClient Create(UserClientCreateApiModel created, int userId);
        int Delete(int id);
        UserClientIndexApiModel GetIndexByUserId(int userId);
    }
}
