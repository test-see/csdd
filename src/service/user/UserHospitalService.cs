using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital.model;
using iservice.user;
using System.Threading.Tasks;

namespace service.user
{
    public class UserHospitalService : IUserHospitalService
    {
        private readonly UserHospitalContext _userHospitalContext;
        public UserHospitalService(UserHospitalContext userHospitalContext)
        {
            _userHospitalContext = userHospitalContext;
        }
        public async Task<PagerResult<UserHospitalListApiModel>> GetPagerListAsync(PagerQuery<UserHospitalListQueryModel> query)
        {
            return await _userHospitalContext.GetPagerListAsync(query);
        }
        public UserHospital Create(UserHospitalCreateApiModel created, int userId)
        {
            return _userHospitalContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _userHospitalContext.Delete(id);
        }

        public async Task<UserHospitalIndexApiModel> GetIndexByUserIdAsync(int userId)
        {
            return await _userHospitalContext.GetIndexByUserIdAsync(userId);
        }
    }
}
