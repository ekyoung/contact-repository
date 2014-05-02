using System;
using System.Text.RegularExpressions;

namespace EthanYoung.ContactRepository
{
    public class PhoneNumber : IValueObject
    {
        private readonly string _value;

        public PhoneNumber(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Argument must be a valid phone number.", "value");
            }

            _value = StripNonNumeric(value);
        }

        public string Value
        {
            get { return _value; }
        }

        public static bool IsValid(string value)
        {
            return StripNonNumeric(value).Length == 10;
        }

        public static bool operator ==(PhoneNumber lhs, PhoneNumber rhs)
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

        public static bool operator !=(PhoneNumber lhs, PhoneNumber rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return string.Compare(((PhoneNumber)obj).Value, Value, true) == 0;            
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Regex.Replace(Value, "(\\d{3})(\\d{3})(\\d{4})", "($1) $2-$3");
        }

        private static string StripNonNumeric(string value)
        {
            string retVal = string.Empty;
            const string pattern = "[^0-9]";
            if (!string.IsNullOrEmpty(value))
            {
                retVal = RemoveWhiteSpace(Regex.Replace(value, pattern, string.Empty));
            }
            return retVal;
        }

        private static string RemoveWhiteSpace(string value)
        {
            const string pattern = @"\s";
            var rgx = new Regex(pattern);
            return !string.IsNullOrEmpty(value) ? rgx.Replace(value, " ") : value;
        }
    }
}