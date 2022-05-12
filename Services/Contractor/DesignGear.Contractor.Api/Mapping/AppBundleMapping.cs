using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            CreateMap<AppBundleDto, VmAppBundle>(MemberList.None);
        }
    }
}