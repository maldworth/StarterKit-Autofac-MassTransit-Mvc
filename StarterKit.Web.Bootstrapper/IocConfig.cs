namespace StarterKit.Web.Bootstrapper
{
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Modules;

    public class IocConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.Load("StarterKit.Web"));

            builder.RegisterModule<BusModule>();

            return builder.Build();
        }
    }
}
