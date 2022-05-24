using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Core.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            //CreateMap<AppBundle, AppBundleDto>(MemberList.None);
            CreateMap<VmAppBundleItem, AppBundleDto>(MemberList.None);
        }
    }
}
