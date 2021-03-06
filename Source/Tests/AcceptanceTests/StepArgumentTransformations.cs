﻿using TechTalk.SpecFlow;

namespace EthanYoung.ContactRepository.Tests.AcceptanceTests
{
    [Binding]
    public class StepArgumentTransformations
    {
        [StepArgumentTransformation]
        public EmailAddress StringToEmailAddress(string value)
        {
            return new EmailAddress(value);
        }

        [StepArgumentTransformation]
        public PhoneNumber StringToPhoneNumber(string value)
        {
            return new PhoneNumber(value);
        }
    }
}