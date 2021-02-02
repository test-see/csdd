using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.prescription;
using irespository.prescription.model;

namespace respository.prescription
{
    public class PrescriptionRespository : IPrescriptionRespository
    {
        private readonly DefaultDbContext _context;
        public PrescriptionRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query)
        {
            throw new System.NotImplementedException();
        }
    }
}
