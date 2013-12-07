using System;
using System.Text.RegularExpressions;

namespace EthanYoung.ContactRepository
{
    public class EmailAddress : IValueObject
    {
        public const string RegexExpression = "\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        private readonly string _value;

        public EmailAddress(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Argument cannot be null or empty.", "value");
            }

            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

        public static bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value) || !(new Regex(RegexExpression)).IsMatch(value))
            {
                return false;
            }

            return true;
        }

        public static bool operator==(EmailAddress lhs, EmailAddress rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if ((object)lhs == null || (object)rhs == null)
            {
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(EmailAddress lhs, EmailAddress rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return string.Compare(((EmailAddress)obj).Value, Value, true) == 0;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}