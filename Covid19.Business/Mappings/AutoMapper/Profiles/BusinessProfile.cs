using AutoMapper;
using Covid19.Entities.Concrete;

namespace Covid19.Business.Mappings.AutoMapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Intent, Intent>();
            CreateMap<Parameter, Parameter>();
            CreateMap<MessageSend, MessageSend>();
        }
    }
}
