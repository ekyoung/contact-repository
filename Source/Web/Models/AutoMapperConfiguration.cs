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
            Mapper.CreateMap<ContactEmailAddress, ContactEmailAddressModel>();
            Mapper.CreateMap<ContactPhoneNumber, ContactPhoneNumberModel>();

            Mapper.CreateMap<IContact, ContactModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.First))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Last))
                .ForMember(dest => dest.EmailAddresses, opt => opt.MapFrom(src => src.EmailAddresses))
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers));

            Mapper.CreateMap<ContactModel, Contact>()
                .AfterMap(MapEncapsulatedCollections)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.FirstName, src.LastName)))
                .ForMember(dest => dest.PrimaryEmailAddress, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryPhoneNumber, opt => opt.Ignore());
        }

        private static void MapEncapsulatedCollections(ContactModel src, Contact dest)
        {
            dest.ClearEmailAddresses();
            src.EmailAddresses.Each(x => dest.SetEmailAddress(new EmailAddress(x.EmailAddress), x.NickName));
            ContactEmailAddressModel primaryEmailAddress = src.EmailAddresses.FirstOrDefault(x => x.IsPrimary);
            if (primaryEmailAddress != null)
            {
                dest.PrimaryEmailAddress = new EmailAddress(primaryEmailAddress.EmailAddress);
            }

            dest.ClearPhoneNumbers();
            src.PhoneNumbers.Each(x => dest.SetPhoneNumber(new PhoneNumber(x.PhoneNumber), x.NickName));
            ContactPhoneNumberModel primaryPhoneNumber = src.PhoneNumbers.FirstOrDefault(x => x.IsPrimary);
            if (primaryPhoneNumber != null)
            {
                dest.PrimaryPhoneNumber = new PhoneNumber(primaryPhoneNumber.PhoneNumber);
            }
        }
    }
}