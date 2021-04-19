using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital;
using irespository.user.hospital.model;
using System.Threading.Tasks;

namespace domain.user
{
    public class UserHospitalContext
    {
        private readonly IUserHospitalRespository _userHospitalRespository;
        public UserHospitalContext(IUserHospitalRespository userHospitalRespository)
        {
            _userHospitalRespository = userHospitalRespository;
        }

        public async Task<PagerResult<UserHospitalListApiModel>> GetPagerListAsync(PagerQuery<UserHospitalListQueryModel> query)
        {
            return await _userHospitalRespository.GetPagerListAsync(query);
        }
        public UserHospital Create(UserHospitalCreateApiModel created, int userId)
        {
            return _userHospitalRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _userHospitalRespository.Delete(id);
        }
        public async Task<UserHospitalIndexApiModel> GetIndexByUserIdAsync(int userId)
        {
            return await _userHospitalRespository.GetIndexByUserIdAsync(userId);
        }
    }
}
