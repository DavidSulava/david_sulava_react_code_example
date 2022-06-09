using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Api.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            CreateMap<AppBundleDto, VmAppBundleItem>(MemberList.None);
            CreateMap<VmAppBundleCreate, CreateAppBundleDto>(MemberList.None);
            CreateMap<VmAppBundleUpdate, UpdateAppBundleDto>(MemberList.None);
        }
    }
}