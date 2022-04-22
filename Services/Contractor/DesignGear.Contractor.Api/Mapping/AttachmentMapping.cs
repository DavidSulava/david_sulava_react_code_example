using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Api.Extensions;

namespace DesignGear.Contractor.Api.Mapping
{
    public class AttachmentMapping : Profile
    {
        public AttachmentMapping()
        {
            CreateMap<IFormFile, AttachmentDto>(MemberList.None)
                .ForMember(x => x.Content, m => m.MapFrom(x => x.ToArray()));
        }
    }
}
