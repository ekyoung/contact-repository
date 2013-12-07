using System;
using IBatisNet.DataMapper.TypeHandlers;

namespace EthanYoung.ContactRepository.Persistence.TypeHandlerCallbacks
{
    public class EmailAddressTypeHandlerCallback : ITypeHandlerCallback
    {
        public void SetParameter(IParameterSetter setter, object parameter)
        {
            if (parameter == null)
            {
                setter.Value = DBNull.Value;
            }
            else
            {
                setter.Value = ((EmailAddress)parameter).Value;
            }
        }

        public object GetResult(IResultGetter getter)
        {
            if (EmailAddress.IsValid(getter.Value as string))
            {
                return new EmailAddress((string)getter.Value); 
            }

            return null;
        }

        public object ValueOf(string s)
        {
            if (EmailAddress.IsValid(s))
            {
                return new EmailAddress(s);
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