using System;
using IBatisNet.DataMapper.TypeHandlers;

namespace EthanYoung.ContactRepository.Persistence.TypeHandlerCallbacks
{
    public class PhoneNumberTypeHandlerCallback : ITypeHandlerCallback
    {
        public void SetParameter(IParameterSetter setter, object parameter)
        {
            if (parameter == null)
            {
                setter.Value = DBNull.Value;
            }
            else
            {
                setter.Value = ((PhoneNumber)parameter).Value;
            }
        }

        public object GetResult(IResultGetter getter)
        {
            if (PhoneNumber.IsValid(getter.Value as string))
            {
                return new PhoneNumber((string)getter.Value);
            }

            return null;
        }

        public object ValueOf(string s)
        {
            if (PhoneNumber.IsValid(s))
            {
                return new PhoneNumber(s);
            }

            return null;
        }

        public object NullValue
        {
            get
            {
                return DBNull.Value;
            }
        }

    }
}