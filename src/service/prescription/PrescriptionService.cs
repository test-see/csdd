using domain.prescription;
using foundation.config;
using foundation.ef5.poco;
using irespository.prescription.model;
using iservice.prescription;

namespace service.prescription
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly PrescriptionContext _prescriptionContext;
        public PrescriptionService(PrescriptionContext prescriptionContext)
        {
            _prescriptionContext = prescriptionContext;
        }
        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            return _prescriptionContext.Create(created, departmentId, userId);
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query)
        {
            return _prescriptionContext.GetPagerList(query);
        }
        public int Submit(int id)
        {
            return _prescriptionContext.Submit(id);
        }
    }
}
