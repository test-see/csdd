using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital.model;
using System.Threading.Tasks;

namespace irespository.user.hospital
{
    public interface IUserHospitalRespository
    {
        Task<PagerResult<UserHospitalListApiModel>> GetPagerListAsync(PagerQuery<UserHospitalListQueryModel> query);
        UserHospital Create(UserHospitalCreateApiModel created, int userId);
        int Delete(int id);
        Task<UserHospitalIndexApiModel> GetIndexByUserIdAsync(int userId);
    }
}
