using foundation.config;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.client.model;

namespace domain.hospital
{
    public class HospitalClientContext
    {
        private readonly IHospitalClientRespository _HospitalClientRespository;
        public HospitalClientContext(IHospitalClientRespository HospitalClientRespository)
        {
            _HospitalClientRespository = HospitalClientRespository;
        }

        public PagerResult<HospitalClientListApiModel> GetPagerList(PagerQuery<HospitalClientListQueryModel> query)
        {
            return _HospitalClientRespository.GetPagerList(query);
        }
        public HospitalClient Create(HospitalClientCreateApiModel created, int userId)
        {
            return _HospitalClientRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _HospitalClientRespository.Delete(id);
        }
        public int Update(int id, HospitalClientUpdateApiModel updated)
        {
            return _HospitalClientRespository.Update(id, updated);
        }
    }
}
