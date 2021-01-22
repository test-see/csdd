using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital.model;

namespace irespository.user.hospital
{
    public interface IUserHospitalRespository
    {
        PagerResult<UserHospitalListApiModel> GetPagerList(PagerQuery<UserHospitalListQueryModel> query);
        UserHospital Create(UserHospitalCreateApiModel created, int userId);
        int Delete(int id);
        UserHospitalIndexApiModel GetIndexByUserId(int userId);
    }
}
