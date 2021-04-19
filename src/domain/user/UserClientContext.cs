using foundation.config;
using foundation.ef5.poco;
using irespository.user.client;
using irespository.user.client.model;
using System.Threading.Tasks;

namespace domain.user
{
    public class UserClientContext
    {
        private readonly IUserClientRespository _userClientRespository;
        public UserClientContext(IUserClientRespository userClientRespository)
        {
            _userClientRespository = userClientRespository;
        }

        public async Task<PagerResult<UserClientListApiModel>> GetPagerListAsync(PagerQuery<UserClientListQueryModel> query)
        {
            return await _userClientRespository.GetPagerListAsync(query);
        }
        public UserClient Create(UserClientCreateApiModel created, int userId)
        {
            return _userClientRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _userClientRespository.Delete(id);
        }
        public async Task<UserClientIndexApiModel> GetIndexByUserIdAsync(int userId)
        {
            return await _userClientRespository.GetIndexByUserIdAsync(userId);
        }
    }
}
