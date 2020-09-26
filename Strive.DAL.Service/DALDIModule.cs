using Autofac;

namespace Strive.DAL.Service
{
    public static class DALDIModule
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<StriveDataRepository>().As<IStriveDataRepository>();            
        }
    }
}
