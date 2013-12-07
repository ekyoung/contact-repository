using System;
using NUnit.Framework;

namespace EthanYoung.ContactRepository.Tests.UnitTests
{
    [TestFixture]
    public class PhoneNumberTests
    {
        private static readonly string[] InvalidValues = {null, string.Empty, "invalid", "222"};
        private static readonly string[] ValidValues = {"1234567890", "(123) 456-7890", "123.456.7890"};

        [Test]
        public void GivenInvalidValue_Constructor_ThrowsException()
        {
            foreach (string invalidValue in InvalidValues)
            {
                string value = invalidValue;
                Assert.Throws<ArgumentException>(() => new PhoneNumber(value));
            }
        }

        [TestCase("1234567890", "1234567890")]
        [TestCase("(123) 456-7890", "1234567890")]
        [TestCase("123.456.7890", "1234567890")]
        public void GivenValidValue_Constructor_StoresCanonicalValue(string validValue, string canonicalValue)
        {
            Assert.AreEqual(canonicalValue, new PhoneNumber(validValue).Value);
        }

        [Test]
        public void GivenInvalidValue_IsValid_ReturnsFalse()
        {
            foreach (string invalidValue in InvalidValues)
            {
                Assert.IsFalse(PhoneNumber.IsValid(invalidValue));
            }
        }

        [Test]
        public void GivenValidValue_IsValid_ReturnsTrue()
        {
            foreach (string validValue in ValidValues)
            {
                Assert.IsTrue(PhoneNumber.IsValid(validValue));
            }
        }

        [Test]
        public void GivenNull_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new PhoneNumber("1234567890") == null);
            Assert.IsFalse(null == new PhoneNumber("1234567890"));
        }

        [Test]
        public void GivenSameObject_Equal_ReturnsTrue()
        {
            var phoneNumber = new PhoneNumber("1234567890");
            Assert.IsTrue(phoneNumber == phoneNumber);
        }

        [Test]
        public void GivenDifferentValue_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new PhoneNumber("1234567890") == new PhoneNumber("0987654321"));
        }

        [Test]
        public void GivenSameValue_Equal_ReturnsTrue()
        {
            const string value = "1234567890";
            Assert.IsTrue(new PhoneNumber(value) == new PhoneNumber(value));
        }

        [Test]
        public void GivenValuesWithDifferentFormattingButSameDigits_Equal_ReturnsTrue()
        {
            Assert.IsTrue(new PhoneNumber("1234567890") == new PhoneNumber("(123) 456-7890"));
        }

        [Test]
        public void ToString_ReturnsFormattedValue()
        {
            Assert.AreEqual("(123) 456-7890", new PhoneNumber("1234567890").ToString());
        }
    }
}