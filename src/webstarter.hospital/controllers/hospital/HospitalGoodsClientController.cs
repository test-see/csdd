﻿using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.hospital
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class HospitalGoodsClientController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public HospitalGoodsClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListHospitalGoodsClientRequest> query)
        {
            var data = await _mediator.RequestPagerListAsync<ListHospitalGoodsClientRequest, ListHospitalGoodsClientResponse>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteHospitalGoodsClient { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(int goodsId, int clientId)
        {
            var created = new CreateHospitalGoodsClient
            {
                HospitalClientId = clientId,
                HospitalGoodsId = goodsId,
                UserId = Profile.Id,
            };
            var data = await _mediator.RequestPipeAsync<CreateHospitalGoodsClient, HospitalGoodsClient>(created);
            return Json(data);
        }

    }
}
