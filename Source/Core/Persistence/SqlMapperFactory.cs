using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using EthanYoung.ContactRepository.Properties;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper.Configuration;

namespace EthanYoung.ContactRepository.Persistence
{
    public class SqlMapperFactory
    {
        public static IBatisNet.DataMapper.ISqlMapper GetMapper()
        {
            string connectionStringName = string.Format("LocalDev-{0}-{1}", Environment.MachineName, Environment.UserName);
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName] ??
                                                                ConfigurationManager.ConnectionStrings[Settings.Default.DefaultConnectionStringName];

            var properties = new NameValueCollection { { "ConnectionString", connectionStringSettings.ConnectionString } };

            var builder = new DomSqlMapBuilder { Properties = properties };

            XmlDocument sqlMapConfig = Resources.GetEmbeddedResourceAsXmlDocument("EthanYoung.ContactRepository.Persistence.sqlMap.config, EthanYoung.ContactRepository");

            //If I can get the type handler registered before the result maps are built, I'm still stuck with having
            //to register this thing for each type, but I don't need a different handler for each type
            //_mapper.TypeHandlerFactory.Register(typeof(MemberStatus), new RegisteredValueObjectTypeHandler());
            //((ResultMap)_mapper.ResultMaps["Member.MemberRM"]).Properties[12].TypeHandler = new RegisteredValueObjectTypeHandler();
            
            return builder.Configure(sqlMapConfig);
        }
    }
}