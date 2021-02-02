using foundation.config;
using foundation.ef5.poco;
using irespository.prescription;
using irespository.prescription.model;

namespace domain.prescription
{
    public class PrescriptionContext
    {
        private readonly IPrescriptionRespository _prescriptionRespository;
        public PrescriptionContext(IPrescriptionRespository prescriptionRespository)
        {
            _prescriptionRespository = prescriptionRespository;
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query)
        {
            return _prescriptionRespository.GetPagerList(query);
        }
        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            return _prescriptionRespository.Create(created, departmentId, userId);
        }
    }
}
