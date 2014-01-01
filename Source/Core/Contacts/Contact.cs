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
    }
}