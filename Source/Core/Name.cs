using System;
using System.Linq;

namespace EthanYoung.ContactRepository
{
    public class Name : IValueObject
    {

        public Name(string first, string last)
        {
            if (string.IsNullOrEmpty(first))
            {
                throw new ArgumentException("Argument cannot be null or empty.", "first");
            }

            if (string.IsNullOrEmpty(last))
            {
                throw new ArgumentException("Argument cannot be null or empty.", "last");
            }

            _first = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(first.ToLower());
            _last = TitleCaseWithApostropheHandling(last);
        }

        private static string TitleCaseWithApostropheHandling(string last)
        {
            return string.Join("'", last.Split('\'').Select(x => System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(x.ToLower())).ToArray());
        }

        private readonly string _first;
        public string First
        {
            get { return _first; }
        }

        private readonly string _last;
        public string Last
        {
            get { return _last; }
        }

        public string LastCommaFirst
        {
            get { return Last + ", " + First; }
        }

        public static bool operator ==(Name lhs, Name rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if ((object) lhs == null || (object) rhs == null)
            {
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Name lhs, Name rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return string.Compare(((Name)obj).First, First, true) == 0 &&
                string.Compare(((Name)obj).Last, Last, true) == 0;
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Last.GetHashCode();
        }

        public override string ToString()
        {
            return First + " " + Last;
        }
    }
}