using System;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace EthanYoung.ContactRepository
{
    public class DependencyRegistry
    {
        private static readonly IWindsorContainer _container = new WindsorContainer(new XmlInterpreter());

        public static T Resolve<T>() where T : class
        {
            var returnObject = _container.Resolve<T>();
            if (returnObject == null)
            {
                throw new Exception(string.Format("Failed to create an object of type '{0}'.  Make sure the object is registered properly.", typeof(T).FullName));
            }
            return returnObject;
        }

        public static T Resolve<T>(string key) where T : class
        {
            var returnObject = _container.Resolve<T>(key);
            if (returnObject == null)
            {
                throw new Exception(string.Format("Failed to create an object of type '{0}'.  Make sure the object is registered properly.", typeof(T).FullName));
            }
            return returnObject;
        }
    }
}