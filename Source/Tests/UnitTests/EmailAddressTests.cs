using System;
using NUnit.Framework;

namespace EthanYoung.ContactRepository.Tests.UnitTests
{
    [TestFixture]
    public class EmailAddressTests
    {
        private static readonly string[] InvalidValues = {null, string.Empty, "invalid"};
        private static readonly string[] ValidValues = {"test@test.com", "example.value@some.fancy.domain.co"};

        [Test]
        public void GivenInvalidValue_Constructor_ThowsException()
        {
            foreach (string invalidValue in InvalidValues)
            {
                string value = invalidValue;
                Assert.Throws<ArgumentException>(() => new EmailAddress(value));
            }
        }

        [Test]
        public void GivenValidValue_Constructor_SavesValue()
        {
            foreach (string validValue in ValidValues)
            {
                var emailAddress = new EmailAddress(validValue);
                Assert.AreEqual(validValue, emailAddress.Value);
            }
        }

        [Test]
        public void GivenInvalidValue_IsValid_ReturnsFalse()
        {
            foreach (string invalidValue in InvalidValues)
            {
                Assert.IsFalse(EmailAddress.IsValid(invalidValue));
            }
        }

        [Test]
        public void GivenValidValue_IsValid_ReturnsTrue()
        {
            foreach (string validValue in ValidValues)
            {
                Assert.IsTrue(EmailAddress.IsValid(validValue));
            }
        }

        [Test]
        public void GivenNull_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new EmailAddress("valid@email.com") == null);
            Assert.IsFalse(null == new EmailAddress("valid@email.com"));
        }

        [Test]
        public void GivenSameObject_Equal_ReturnsTrue()
        {
            var emailAddress = new EmailAddress("valid@email.com");
            Assert.IsTrue(emailAddress == emailAddress);
        }

        [Test]
        public void GivenDifferentValue_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new EmailAddress("email1@address.com") == new EmailAddress("email2@address.com") );
        }

        [Test]
        public void GivenSameValue_Equal_ReturnsTrue()
        {
            const string value = "email@address.com";
            Assert.IsTrue(new EmailAddress(value) == new EmailAddress(value));
        }

        [Test]
        public void GivenValuesWithDifferentCases_Equal_ReturnsTrue()
        {
            Assert.IsTrue(new EmailAddress("EMAIL@ADDRESS.com") == new EmailAddress("email@address.com"));
        }

        [Test]
        public void ToString_ReturnsValue()
        {
            var emailAddress = new EmailAddress("email@address.com");
            Assert.AreEqual(emailAddress.Value, emailAddress.ToString());
        }
    }
}