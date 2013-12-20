using AutoMapper;
using EthanYoung.ContactRepository;
using EthanYoung.ContactRepository.Contacts;

namespace Web.Models
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<IContact, ContactModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.First))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Last));

            Mapper.CreateMap<ContactModel, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.FirstName, src.LastName)))
                .ForMember(dest => dest.PrimaryEmailAddress, opt => opt.Ignore());
        }
    }
}