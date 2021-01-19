using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital.model;

namespace iservice.user
{
    public interface IUserHospitalService
    {
        PagerResult<UserHospitalListApiModel> GetPagerList(PagerQuery<UserHospitalListQueryModel> query);
        UserHospital Create(UserHospitalCreateApiModel created, int userId);
        int Delete(int id);
    }
}
