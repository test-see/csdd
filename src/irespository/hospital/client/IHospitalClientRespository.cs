using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.client.model;

namespace irespository.hospital
{
    public interface IHospitalClientRespository
    {
        PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query);
        HospitalClient Create(HospitalClientCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, HospitalClientUpdateApiModel updated);
        HospitalClientValueModel GetValue(int id);
    }
}
