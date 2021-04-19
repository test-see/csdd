using foundation.config;
using foundation.ef5.poco;
using irespository.user.client.model;
using System.Threading.Tasks;

namespace irespository.user.client
{
    public interface IUserClientRespository
    {
        Task<PagerResult<UserClientListApiModel>> GetPagerListAsync(PagerQuery<UserClientListQueryModel> query);
        UserClient Create(UserClientCreateApiModel created, int userId);
        int Delete(int id);
        Task<UserClientIndexApiModel> GetIndexByUserIdAsync(int userId);
    }
}
