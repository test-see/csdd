﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
using iservice.client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireClientRole")]
    [Route("api/ClientMapping")]
    public class Client2HospitalClientController : DefaultControllerBase
    {
        private readonly IClient2HospitalClientService _clientMappingService;
        public Client2HospitalClientController(IClient2HospitalClientService clientMappingService)
        {
            _clientMappingService = clientMappingService;
        }
        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<Client2HospitalClientListQueryModel> query)
        {
            query.Query = query.Query ?? new Client2HospitalClientListQueryModel { };
            query.Query.ClientId = Client.Id;
            var data = _clientMappingService.GetPagerList(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _clientMappingService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(Client2HospitalClientCreateApiModel created)
        {
            created.ClientId = Client.Id;
            var data = _clientMappingService.Create(created, Profile.Id);
            return Json(data);
        }

    }
}