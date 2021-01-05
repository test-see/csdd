using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.model;

namespace irespository.hospital
{
    public interface IHospitalRespository
    {
        PagerResult<HospitalListApiModel> GetPagerList(PagerQuery<HospitalListQueryModel> query);
        Hospital Create(HospitalCreateApiModel created, int userId);
        int Delete(int id);
        int Update(HospitalUpdateApiModel updated);
    }
}
