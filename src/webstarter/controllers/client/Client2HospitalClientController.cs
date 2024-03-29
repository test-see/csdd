﻿using client.application.v2;
using client.application.v2.view;
using client.domain.v2.entity;
using client.storage.qurable.v2.data;
using csdd.Controllers.Shared;
using foundation.config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("api/ClientMapping")]
    public class Client2HospitalClientController : DefaultControllerBase
    {
        private readonly ClientApplication _clientApplication;
        public Client2HospitalClientController(ClientApplication clientApplication)
        {
            _clientApplication = clientApplication;
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<OkMessage<int>> DeleteAsync(int id)
        {
            await _clientApplication.HospitalClientApplication.DeleteAsync(id);
            return OkMessage(id);
        }
        [HttpPost]
        [Route("add")]
        public async Task<OkMessage<int>> PostAsync(Client2HospitalClientCreation payload)
        {
            var data = await _clientApplication.HospitalClientApplication.CreateAsync(payload, UserId);
            return OkMessage(data);
        }
        [HttpPost]
        [Route("list")]
        public OkMessage<PagerResult<GetClient2HospitalClient>> GetByPage(PagerQuery<Client2HospitalClientQurable> payload)
        {
            var data = _clientApplication.HospitalClientApplication.GetByPage(payload);
            return OkMessage(data);
        }
    }
}
