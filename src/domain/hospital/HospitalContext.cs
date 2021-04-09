using foundation.config;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.model;
using irespository.hospital.profile.model;
using System.Linq;

namespace domain.hospital
{
    public class HospitalContext
    {
        private readonly IHospitalRespository _hospitalRespository;
        public HospitalContext(IHospitalRespository HospitalRespository)
        {
            _hospitalRespository = HospitalRespository;
        }

        public PagerResult<HospitalListApiModel> GetPagerList(PagerQuery<HospitalListQueryModel> query)
        {
            return _hospitalRespository.GetPagerList(query);
        }
        public Hospital Create(HospitalCreateApiModel created, int userId)
        {
            return _hospitalRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _hospitalRespository.Delete(id);
        }
        public int Update(int id, HospitalUpdateApiModel updated)
        {
            return _hospitalRespository.Update(id, updated);
        }
        public GetHospitalResponse GetValue(int id)
        {
            return _hospitalRespository.GetValue(new int[] { id }).FirstOrDefault();
        }
    }
}
