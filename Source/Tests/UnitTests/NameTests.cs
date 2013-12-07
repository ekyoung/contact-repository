using System;
using NUnit.Framework;

namespace EthanYoung.ContactRepository.Tests.UnitTests
{
    [TestFixture]
    public class NameTests
    {
        [Test]
        public void GivenValuesForFirstAndLast_Constructor_SavesValues()
        {
            const string first = "First";
            const string last = "Last";

            var name = new Name(first, last);

            Assert.AreEqual(first, name.First);
            Assert.AreEqual(last, name.Last);
        }

        [TestCase("first", "last", "First", "Last")]
        [TestCase("SHAMUS", "O'FLannigan", "Shamus", "O'Flannigan")]
        public void GivenValuesWithImproperCasing_Constructor_CorrectsThem(string constructorFirst, string constructorLast, string propertyFirst, string propertyLast)
        {
            var name = new Name(constructorFirst, constructorLast);
            
            Assert.AreEqual(propertyFirst, name.First);
            Assert.AreEqual(propertyLast, name.Last);
        }

        [TestCase(null)]
        [TestCase("")]
        public void GivenNullOrEmptyFirstName_Constructor_ThrowsException(string invalidValue)
        {
            Assert.Throws<ArgumentException>(() => new Name(invalidValue, "Last"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GivenNullOrEmptyLastName_Constructor_ThrowsException(string invalidValue)
        {
            Assert.Throws<ArgumentException>(() => new Name("First", invalidValue));
        }

        [Test]
        public void ToString_ReturnsFirstSpaceLast()
        {
            const string first = "First";
            const string last = "Last";

            var name = new Name(first, last);

            Assert.AreEqual(first + " " + last, name.ToString());
        }

        [Test]
        public void LastCommaFirst_ReturnsLastCommaSpaceFirst()
        {
            const string first = "First";
            const string last = "Last";

            var name = new Name(first, last);

            Assert.AreEqual(last + ", " + first, name.LastCommaFirst);
        }

        [Test]
        public void GivenNull_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new Name("first", "last") == null);
            Assert.IsFalse(null == new Name("first", "last"));
        }

        [Test]
        public void GivenSameObject_Equal_ReturnsTrue()
        {
            var name = new Name("first", "last");
            Assert.IsTrue(name == name);
        }

        [Test]
        public void GivenDifferentFirstName_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new Name("first", "last") == new Name("notfirst", "last"));
        }

        [Test]
        public void GivenDifferentLastName_Equal_ReturnsFalse()
        {
            Assert.IsFalse(new Name("first", "last") == new Name("first", "notlast"));
        }

        [Test]
        public void GivenSameValue_Equal_ReturnsTrue()
        {
            const string first = "First";
            const string last = "Last";

            Assert.IsTrue(new Name(first, last) == new Name(first, last));
        }

        [Test]
        public void GivenValuesWithDifferentCases_Equal_ReturnsTrue()
        {
            Assert.IsTrue(new Name("first", "last") == new Name("First", "LAST"));
        }

    }
}