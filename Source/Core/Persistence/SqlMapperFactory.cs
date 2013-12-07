using System.Collections.Specialized;
using System.Xml;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper.Configuration;

namespace EthanYoung.ContactRepository.Persistence
{
    public class SqlMapperFactory
    {
        private static volatile IBatisNet.DataMapper.ISqlMapper _mapper;

        static SqlMapperFactory()
        {
            var properties = new NameValueCollection { { "ConnectionString", @"Data Source=localhost;Initial Catalog=ContactRepositoryTests;Integrated Security=True" } };

            var builder = new DomSqlMapBuilder { Properties = properties };

            XmlDocument sqlMapConfig = Resources.GetEmbeddedResourceAsXmlDocument("EthanYoung.ContactRepository.Persistence.sqlMap.config, EthanYoung.ContactRepository");
            _mapper = builder.Configure(sqlMapConfig);
            //If I can get the type handler registered before the result maps are built, I'm still stuck with having
            //to register this thing for each type, but I don't need a different handler for each type
            //_mapper.TypeHandlerFactory.Register(typeof(MemberStatus), new RegisteredValueObjectTypeHandler());
            //((ResultMap)_mapper.ResultMaps["Member.MemberRM"]).Properties[12].TypeHandler = new RegisteredValueObjectTypeHandler();
        }

        public static IBatisNet.DataMapper.ISqlMapper GetMapper()
        {
            return _mapper;
        }
    }
}