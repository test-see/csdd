using domain.user;
using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital.model;
using iservice.user;

namespace service.user
{
    public class UserHospitalService : IUserHospitalService
    {
        private readonly UserHospitalContext _userHospitalContext;
        public UserHospitalService(UserHospitalContext userHospitalContext)
        {
            _userHospitalContext = userHospitalContext;
        }
        public PagerResult<UserHospitalListApiModel> GetPagerList(PagerQuery<UserHospitalListQueryModel> query)
        {
            return _userHospitalContext.GetPagerList(query);
        }
        public UserHospital Create(UserHospitalCreateApiModel created, int userId)
        {
            return _userHospitalContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _userHospitalContext.Delete(id);
        }

        public UserHospitalIndexApiModel GetIndexByUserId(int userId)
        {
            return _userHospitalContext.GetIndexByUserId(userId);
        }
    }
}
