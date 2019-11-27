using AutoMapper;
using CoreCodeCamp.Models;

namespace CoreCodeCamp.Data
{
    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            this.CreateMap<Talk, TalkModels>()
                .ForMember(t => t.FirstName, c => c.MapFrom(m => m.Speaker.FirstName))
                .ForMember(t => t.LastName, c => c.MapFrom(m => m.Speaker.LastName))
                .ForMember(t => t.MiddleName, c => c.MapFrom(m => m.Speaker.MiddleName))
                .ForMember(t => t.Company, c => c.MapFrom(m => m.Speaker.Company))
                .ForMember(t => t.CompanyUrl, c => c.MapFrom(m => m.Speaker.CompanyUrl))
                .ForMember(t => t.BlogUrl, c => c.MapFrom(m => m.Speaker.BlogUrl))
                .ForMember(t => t.Twitter, c => c.MapFrom(m => m.Speaker.Twitter))
                .ForMember(t => t.GitHub, c => c.MapFrom(m => m.Speaker.GitHub)).ReverseMap();
        }
    }
}