using System.Linq;
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
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Last))
                .ForMember(dest => dest.EmailAddresses, opt => opt.MapFrom(src => src.EmailAddresses.Select(x => new ContactEmailAddressModel { EmailAddress = x.EmailAddress.Value, NickName = x.Nickname, IsPrimary = x.IsPrimary })));

            Mapper.CreateMap<ContactModel, Contact>()
                .AfterMap(MapEncapsulatedCollections)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.FirstName, src.LastName)))
                .ForMember(dest => dest.PrimaryEmailAddress, opt => opt.Ignore());
        }

        private static void MapEncapsulatedCollections(ContactModel src, Contact dest)
        {
            src.EmailAddresses.Each(x => dest.SetEmailAddress(new EmailAddress(x.EmailAddress), x.NickName));
            ContactEmailAddressModel primaryEmailAddress = src.EmailAddresses.FirstOrDefault(x => x.IsPrimary);
            if (primaryEmailAddress != null)
            {
                dest.PrimaryEmailAddress = new EmailAddress(primaryEmailAddress.EmailAddress);
            }
        }
    }
}