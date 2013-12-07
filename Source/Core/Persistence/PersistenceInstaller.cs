using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IBatisNet.DataMapper;

namespace EthanYoung.ContactRepository.Persistence
{
    public class PersistenceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISqlMapper>().Instance(SqlMapperFactory.GetMapper()).Named("EthanYoung.ContactRepository.Persistence.ISqlMapper"));
            container.Register(Classes.FromThisAssembly().BasedOn<QueryExecutor>().WithService.DefaultInterfaces());
            container.Register(Classes.FromThisAssembly().BasedOn<IRepository>().WithService.DefaultInterfaces());
        }
    }
}