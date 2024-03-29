﻿using client.domain.v2.entity;
using foundation.ef5.poco;
using storage.adapter.v2.client;
using System.Threading.Tasks;

namespace domain.v2.client
{
    public class ClientService
    {
        private readonly IClientRespository _clientRespository;
        public Client2HospitalClientService HospitalClientService { get; private set; }
        public ClientGoodsService GoodsService { get; private set; }
        public ClientService(IClientRespository clientRespository,
            Client2HospitalClientService hospitalClientService,
           ClientGoodsService goodsService)
        {
            _clientRespository = clientRespository;
            HospitalClientService = hospitalClientService;
            GoodsService = goodsService;
        }
        public async Task<Client> CreateAsync(ClientCreation payload, int userId)
        {
            return await _clientRespository.CreateAsync(new Client
            {
                CreateUserId = userId,
                Name = payload.Name,
            });
        }
        public async Task<Client> UpdateAsync(ClientUpdation payload)
        {
            return await _clientRespository.UpdateAsync(new Client
            {
                Name = payload.Name,
            });
        }
        public async Task DeleteAsync(int id)
        {
            await HospitalClientService.DeleteAllByClientIdAsync(id);
            await _clientRespository.DeleteAsync(id);
        }
    }
}
