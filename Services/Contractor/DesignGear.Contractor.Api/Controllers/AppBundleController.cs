using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Authorize]
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

        [HttpGet]
        public async Task<ICollection<VmAppBundle>> AppBundleListAsync()
        {
            return (await _appBundleService.GetAppBundlesAsync()).MapTo<ICollection<VmAppBundle>>(_mapper);
        }
    }
}
