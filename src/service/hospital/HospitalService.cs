using domain.hospital;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.model;
using iservice.hospital;

namespace service.hospital
{
    public class HospitalService : IHospitalService
    {
        private readonly HospitalContext _hospitalContext;
        public HospitalService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public PagerResult<HospitalListApiModel> GetPagerList(PagerQuery<HospitalListQueryModel> query)
        {
            return _hospitalContext.GetPagerList(query);
        }
        public Hospital Create(HospitalCreateApiModel created, int userId)
        {
            return _hospitalContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _hospitalContext.Delete(id);
        }

        public int Update(int id, HospitalUpdateApiModel updated)
        {
            return _hospitalContext.Update(id, updated);
        }
    }
}
