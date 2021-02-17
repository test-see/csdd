using foundation.config;
using foundation.ef5.poco;
using irespository.user.client;
using irespository.user.client.model;

namespace domain.user
{
    public class UserClientContext
    {
        private readonly IUserClientRespository _userClientRespository;
        public UserClientContext(IUserClientRespository userClientRespository)
        {
            _userClientRespository = userClientRespository;
        }

        public PagerResult<UserClientListApiModel> GetPagerList(PagerQuery<UserClientListQueryModel> query)
        {
            return _userClientRespository.GetPagerList(query);
        }
        public UserClient Create(UserClientCreateApiModel created, int userId)
        {
            return _userClientRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _userClientRespository.Delete(id);
        }
        public UserClientIndexApiModel GetIndexByUserId(int userId)
        {
            return _userClientRespository.GetIndexByUserId(userId);
        }
    }
}
