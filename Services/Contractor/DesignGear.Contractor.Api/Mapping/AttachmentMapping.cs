using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class AttachmentMapping : Profile
    {
        public AttachmentMapping()
        {
            CreateMap<IFormFile, AttachmentDto>(MemberList.None)
                .ForMember(x => x.Content, m => m.MapFrom(x => x.ToArray()));
            CreateMap<AttachmentDto, VmAttachment>(MemberList.None);
        }
    }
}
