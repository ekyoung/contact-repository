using System;
using System.Collections.Generic;
using System.Linq;

namespace EthanYoung.ContactRepository.Contacts
{
    public class Contact : IContact
    {
        public long? Id { get; set; }
        public Guid Identifier { get; set; }
        public Name Name { get; set; }

        protected readonly List<ContactEmailAddress> _emailAddresses = new List<ContactEmailAddress>();
        public IReadOnlyCollection<ContactEmailAddress> EmailAddresses
        {
            get { return _emailAddresses.AsReadOnly(); }
        }

        public EmailAddress PrimaryEmailAddress
        {
            get
            {
                var contactEmailAddress = _emailAddresses.FirstOrDefault(x => x.IsPrimary);
                return contactEmailAddress == null ? null : contactEmailAddress.EmailAddress;
            }
            set
            {
                LookupOrAddContactEmailAddress(value);
                foreach (var emailAddress in EmailAddresses)
                {
                    emailAddress.IsPrimary = emailAddress.EmailAddress == value;
                }
            }
        }

        public void SetEmailAddress(EmailAddress emailAddress, string nickname)
        {
            var contactEmailAddress = LookupOrAddContactEmailAddress(emailAddress);

            contactEmailAddress.Nickname = nickname;
            if (_emailAddresses.Count == 1)
            {
                contactEmailAddress.IsPrimary = true;
            }
        }

        public void ClearEmailAddresses()
        {
            _emailAddresses.Clear();
        }

        private ContactEmailAddress LookupOrAddContactEmailAddress(EmailAddress emailAddress)
        {
            var contactEmailAddress = _emailAddresses.FirstOrDefault(x => x.EmailAddress == emailAddress);

            if (contactEmailAddress == null)
            {
                contactEmailAddress = new ContactEmailAddress
                {
                    EmailAddress = emailAddress
                };

                _emailAddresses.Add(contactEmailAddress);
            }

            return contactEmailAddress;
        }

        private readonly List<ContactPhoneNumber> _phoneNumbers = new List<ContactPhoneNumber>();
        public IReadOnlyCollection<ContactPhoneNumber> PhoneNumbers
        {
            get { return _phoneNumbers.AsReadOnly(); }
        }

        public PhoneNumber PrimaryPhoneNumber
        {
            get
            {
                var contactPhoneNumber = _phoneNumbers.FirstOrDefault(x => x.IsPrimary);
                return contactPhoneNumber == null ? null : contactPhoneNumber.PhoneNumber;
            }
            set
            {
                LookupOrAddContactPhoneNumber(value);
                foreach (var phoneNumber in PhoneNumbers)
                {
                    phoneNumber.IsPrimary = phoneNumber.PhoneNumber == value;
                }
            }
        }

        public void SetPhoneNumber(PhoneNumber phoneNumber, string nickname)
        {
            var contactPhoneNumber = LookupOrAddContactPhoneNumber(phoneNumber);

            contactPhoneNumber.Nickname = nickname;
            if (_phoneNumbers.Count == 1)
            {
                contactPhoneNumber.IsPrimary = true;
            }
        }

        public void ClearPhoneNumbers()
        {
            _phoneNumbers.Clear();
        }

        private ContactPhoneNumber LookupOrAddContactPhoneNumber(PhoneNumber phoneNumber)
        {
            var contactPhoneNumber = _phoneNumbers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

            if (contactPhoneNumber == null)
            {
                contactPhoneNumber = new ContactPhoneNumber
                {
                    PhoneNumber = phoneNumber
                };

                _phoneNumbers.Add(contactPhoneNumber);
            }

            return contactPhoneNumber;
        }
    }

    public interface IContact
    {
        long? Id { get; set; }
        Guid Identifier { get; set; }
        Name Name { get; set; }
        
        EmailAddress PrimaryEmailAddress { get; set; }
        IReadOnlyCollection<ContactEmailAddress> EmailAddresses { get; }
        void SetEmailAddress(EmailAddress emailAddress, string nickname);
        void ClearEmailAddresses();

        PhoneNumber PrimaryPhoneNumber { get; set; }
        IReadOnlyCollection<ContactPhoneNumber> PhoneNumbers { get; }
        void SetPhoneNumber(PhoneNumber phoneNumber, string nickname);
        void ClearPhoneNumbers();
    }
}