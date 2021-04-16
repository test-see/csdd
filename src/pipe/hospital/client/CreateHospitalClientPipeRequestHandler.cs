﻿using domain.hospital;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateHospitalClientPipeRequestHandler : IRequestHandler<PipeRequest<CreateHospitalClient>, HospitalClient>
    {
        private readonly HospitalClientService _service;
        public CreateHospitalClientPipeRequestHandler(HospitalClientService service)
        {
            _service = service;
        }
        public async Task<HospitalClient> Handle(IReceiveContext<PipeRequest<CreateHospitalClient>> context, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(context.Message.Payload);
        }
    }
}