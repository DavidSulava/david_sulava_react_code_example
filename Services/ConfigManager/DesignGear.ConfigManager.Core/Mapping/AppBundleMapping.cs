using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using DesignGear.Common.Extensions;

namespace DesignGear.ConfigManager.Core.Mapping
{
    public class AppBundleMapping : Profile
    {
        public AppBundleMapping()
        {
            CreateMap<AppBundle, AppBundleDto>(MemberList.None);
            CreateMap<AppBundleDto, AppBundle>(MemberList.None);
            CreateMap<CreateAppBundleDto, AppBundle>(MemberList.None)
                .ForMember(x => x.FileName, m => m.MapFrom(x => x.File.FileName))
                .ForMember(x => x.Content, m => m.MapFrom(x => x.File.ToArray()));
            CreateMap<UpdateAppBundleDto, AppBundle>(MemberList.None);
        }
    }
}
