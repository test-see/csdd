using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalService
    {
        private readonly IMediator _mediator;
        public HospitalService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Hospital> UpdateAsync(UpdateHospital updated)
        {
            return await _mediator.RequestAsync<StorageRequest<UpdateHospital>, Hospital>(new StorageRequest<UpdateHospital>(updated));
        }

        public async Task<Hospital> CreateAsync(CreateHospital created)
        {
            return await _mediator.RequestAsync<StorageRequest<CreateHospital>, Hospital>(new StorageRequest<CreateHospital>(created));
        }
        public async Task DeleteAsync(DeleteHospital deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteHospital>(deleted));
        }
    }
}
