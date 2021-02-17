using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.user.client.model;
using iservice.user;

namespace service.user
{
    public class UserClientService : IUserClientService
    {
        private readonly UserClientContext _userClientContext;
        public UserClientService(UserClientContext userClientContext)
        {
            _userClientContext = userClientContext;
        }
        public PagerResult<UserClientListApiModel> GetPagerList(PagerQuery<UserClientListQueryModel> query)
        {
            return _userClientContext.GetPagerList(query);
        }
        public UserClient Create(UserClientCreateApiModel created, int userId)
        {
            return _userClientContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _userClientContext.Delete(id);
        }

        public UserClientIndexApiModel GetIndexByUserId(int userId)
        {
            return _userClientContext.GetIndexByUserId(userId);
        }
    }
}
