using foundation.config;
using foundation.ef5.poco;
using irespository.user.hospital;
using irespository.user.hospital.model;

namespace domain.user
{
    public class UserHospitalContext
    {
        private readonly IUserHospitalRespository _userHospitalRespository;
        public UserHospitalContext(IUserHospitalRespository userHospitalRespository)
        {
            _userHospitalRespository = userHospitalRespository;
        }

        public PagerResult<UserHospitalListApiModel> GetPagerList(PagerQuery<UserHospitalListQueryModel> query)
        {
            return _userHospitalRespository.GetPagerList(query);
        }
        public UserHospital Create(UserHospitalCreateApiModel created, int userId)
        {
            return _userHospitalRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _userHospitalRespository.Delete(id);
        }
    }
}
