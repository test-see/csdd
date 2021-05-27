using storage.adapter.v2.client;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.v2.client
{
    public class ClientService
    {
        private readonly IClientRespository _clientRespository;
        public ClientService(IClientRespository clientRespository)
        {
            _clientRespository = _clientRespository;
        }
        //public async Task<Client> CreateAsync(CreateClientRequest created)
        //{
        //    return await _mediator.RequestAsync<CreateClientRequest, Client>(created);
        //}
    }
}
