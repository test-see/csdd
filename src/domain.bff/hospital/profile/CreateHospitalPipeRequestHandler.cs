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
    public class CreateHospitalPipeRequestHandler : IRequestHandler<Pipe<CreateHospitalRequest>, Hospital>
    {
        private readonly HospitalService _service;
        public CreateHospitalPipeRequestHandler(HospitalService service)
        {
            _service = service;
        }
        public async Task<Hospital> Handle(IReceiveContext<Pipe<CreateHospitalRequest>> context, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(context.Message.Payload);
        }
    }
}
