using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalDepartmentService
    {
        private readonly IMediator _mediator;
        public HospitalDepartmentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<HospitalDepartment> UpdateAsync(UpdateHospitalDepartment updated)
        {
            return await _mediator.RequestSingleAsync<UpdateHospitalDepartment, HospitalDepartment>(updated);
        }

        public async Task<HospitalDepartment> CreateAsync(CreateHospitalDepartment created)
        {
            return await _mediator.RequestSingleAsync<CreateHospitalDepartment, HospitalDepartment>(created);
        }
        public async Task DeleteAsync(DeleteHospitalDepartment deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
