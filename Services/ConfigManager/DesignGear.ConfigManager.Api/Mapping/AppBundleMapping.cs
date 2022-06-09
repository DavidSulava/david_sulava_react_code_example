using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.ConfigManager.Api.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            CreateMap<VmAppBundleCreate, CreateAppBundleDto>(MemberList.None);
            CreateMap<VmAppBundleUpdate, UpdateAppBundleDto>(MemberList.None);
            CreateMap<AppBundleDto, VmAppBundleItem>(MemberList.None);
        }
    }
}