using AutoMapper;
using CoreCodeCamp.Models;

namespace CoreCodeCamp.Data.Entities
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModels>().ForMember(c => c.Venue, l => l.MapFrom(m => m.Location.VenueName)).ReverseMap();
        }
    }
}