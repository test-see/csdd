using domain.hospital;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.client.model;
using iservice.hospital;

namespace service.hospital
{
    public class HospitalClientService : IHospitalClientService
    {
        private readonly HospitalClientContext _hospitalClientContext;
        public HospitalClientService(HospitalClientContext hospitalClientContext)
        {
            _hospitalClientContext = hospitalClientContext;
        }
        public PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query)
        {
            return _hospitalClientContext.GetPagerList(query);
        }
        public HospitalClient Create(HospitalClientCreateApiModel created, int userId)
        {
            return _hospitalClientContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _hospitalClientContext.Delete(id);
        }

        public int Update(int id, HospitalClientUpdateApiModel updated, int userId)
        {
            return _hospitalClientContext.Update(id, updated);
        }
    }
}
