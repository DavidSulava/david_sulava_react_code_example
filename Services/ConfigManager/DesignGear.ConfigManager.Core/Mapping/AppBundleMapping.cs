using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.ConfigManager.Core.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            CreateMap<AppBundle, AppBundleDto>(MemberList.None);
        }
    }
}
