using Autofac;
using Strive.BLL.Service;
using Strive.DAL.Service;
namespace Strive.BLL.Service
{ 
    public static class BLDIModule
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<StriveBLService>().As<IStriveBLService>();
            DALDIModule.RegisterServices(builder);            
        }
    }
}
