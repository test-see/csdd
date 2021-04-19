using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.user.client.model;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class UserClientService : IUserClientService
    {
        private readonly UserClientContext _userClientContext;
        public UserClientService(UserClientContext userClientContext)
        {
            _userClientContext = userClientContext;
        }
        public async Task<PagerResult<UserClientListApiModel>> GetPagerListAsync(PagerQuery<UserClientListQueryModel> query)
        {
            return await _userClientContext.GetPagerListAsync(query);
        }
        public UserClient Create(UserClientCreateApiModel created, int userId)
        {
            return _userClientContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _userClientContext.Delete(id);
        }

        public async Task<UserClientIndexApiModel> GetIndexByUserIdAsync(int userId)
        {
            return await _userClientContext.GetIndexByUserIdAsync(userId);
        }
    }
}
