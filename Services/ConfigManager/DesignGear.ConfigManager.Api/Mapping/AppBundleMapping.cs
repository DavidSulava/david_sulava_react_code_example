using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.ConfigManager.Api.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            CreateMap<AppBundleDto, VmAppBundleItem>(MemberList.None);
        }
    }
}