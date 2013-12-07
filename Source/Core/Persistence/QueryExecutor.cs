using IBatisNet.DataMapper;

namespace EthanYoung.ContactRepository.Persistence
{
    public abstract class QueryExecutor
    {
        private readonly ISqlMapper _sqlMapper;

        protected QueryExecutor()
        {
            _sqlMapper = DependencyRegistry.Resolve<ISqlMapper>("EthanYoung.ContactRepository.Persistence.ISqlMapper");
        }

        protected ISqlMapper SqlMapper
        {
            get { return _sqlMapper; }
        }
    }
}