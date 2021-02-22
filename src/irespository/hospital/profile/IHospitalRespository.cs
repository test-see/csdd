using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.model;
using irespository.hospital.profile.model;

namespace irespository.hospital
{
    public interface IHospitalRespository
    {
        PagerResult<HospitalListApiModel> GetPagerList(PagerQuery<HospitalListQueryModel> query);
        Hospital Create(HospitalCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, HospitalUpdateApiModel updated);
        HospitalValueModel GetValue(int id);
    }
}
