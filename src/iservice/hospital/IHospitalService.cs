using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.model;

namespace iservice.hospital
{
    public interface IHospitalService
    {
        PagerResult<HospitalListApiModel> GetPagerList(PagerQuery<HospitalListQueryModel> query);
        Hospital Create(HospitalCreateApiModel created, int userId);
        int Delete(int id);
        int Update(HospitalUpdateApiModel updated);
    }
}
