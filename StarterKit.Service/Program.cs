namespace StarterKit.Service
{
    using System.IO;
    using System.Text;
    using log4net.Config;
    using Topshelf;
    using Topshelf.Logging;
    using Autofac;
    using MassTransit.Log4NetIntegration.Logging;


    class Program
    {
        static int Main(string[] args)
        {
            // Loads the config from our App.config
            XmlConfigurator.Configure();

            // Topshelf to use Log4Net
            Log4NetLogWriterFactory.Use();

            // MassTransit to use Log4Net
            Log4NetLogger.Use();

            // Ioc Helper method for Autofac
            var container = IocConfig.RegisterDependencies();

            return (int)HostFactory.Run(cfg =>
            {
                cfg.Service(s => container.Resolve<MyService>());

                cfg.RunAsLocalSystem();
            });
        }
    }
}