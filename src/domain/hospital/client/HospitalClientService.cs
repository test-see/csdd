using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalClientService
    {
        private readonly IMediator _mediator;
        public HospitalClientService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<HospitalClient> UpdateAsync(UpdateHospitalClient updated)
        {
            return await _mediator.RequestAsync<StorageRequest<UpdateHospitalClient>, HospitalClient>(new StorageRequest<UpdateHospitalClient>(updated));
        }

        public async Task<HospitalClient> CreateAsync(CreateHospitalClient created)
        {
            return await _mediator.RequestAsync<StorageRequest<CreateHospitalClient>, HospitalClient>(new StorageRequest<CreateHospitalClient>(created));
        }
        public async Task DeleteAsync(DeleteHospitalClient deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteHospitalClient>(deleted));
        }
    }
}
