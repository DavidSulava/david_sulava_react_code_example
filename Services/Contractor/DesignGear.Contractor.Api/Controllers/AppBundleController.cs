﻿using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = "OrganizationSelected")]
    [Route("[controller]")]
    public class AppBundleController : ControllerBase
    {
        private readonly IAppBundleService _appBundleService;
        private readonly IMapper _mapper;

        public AppBundleController(IAppBundleService appBundleService, IMapper mapper)
        {
            _appBundleService = appBundleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Guid> CreateAppBundleAsync([FromForm] VmAppBundleCreate create)
        {
            return await _appBundleService.CreateAppBundleAsync(create.MapTo<CreateAppBundleDto>(_mapper));
        }

        [HttpPut]
        public async Task UpdateAppBundleAsync([FromForm] VmAppBundleUpdate update)
        {
            await _appBundleService.UpdateAppBundleAsync(update.MapTo<UpdateAppBundleDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveAppBundleAsync(Guid id)
        {
            await _appBundleService.RemoveAppBundleAsync(id);
        }

        [HttpGet]
        public async Task<ICollection<VmAppBundleItem>> AppBundleListAsync()
        {
            return (await _appBundleService.GetAppBundlesAsync()).MapTo<ICollection<VmAppBundleItem>>(_mapper);
        }

        [HttpGet("{id}")]
        public async Task<VmAppBundleItem> GetAppBundleAsync([FromRoute] Guid id)
        {
            return (await _appBundleService.GetAppBundleAsync(id)).MapTo<VmAppBundleItem>(_mapper);
        }
    }
}
