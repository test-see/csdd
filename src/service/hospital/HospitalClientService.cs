using domain.hospital;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.client.model;
using iservice.hospital;

namespace service.hospital
{
    public class HospitalClientService : IHospitalClientService
    {
        private readonly HospitalClientContext _HospitalClientContext;
        public HospitalClientService(HospitalClientContext HospitalClientContext)
        {
            _HospitalClientContext = HospitalClientContext;
        }
        public PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query)
        {
            return _HospitalClientContext.GetPagerList(query);
        }
        public HospitalClient Create(HospitalClientCreateApiModel created, int userId)
        {
            return _HospitalClientContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _HospitalClientContext.Delete(id);
        }

        public int Update(int id, HospitalClientUpdateApiModel updated, int userId)
        {
            return _HospitalClientContext.Update(id, updated);
        }
    }
}
