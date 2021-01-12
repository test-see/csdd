using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.client.model;

namespace iservice.hospital
{
    public interface IHospitalClientService
    {
        PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query);
        HospitalClient Create(HospitalClientCreateApiModel created, int userId);
        int Delete(int id);
        int Update(HospitalClientUpdateApiModel updated, int userId);
    }
}
