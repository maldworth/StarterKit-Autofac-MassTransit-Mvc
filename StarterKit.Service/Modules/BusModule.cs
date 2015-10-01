namespace StarterKit.Service.Modules
{
    using System;
    using System.Configuration;
    using Autofac;
    using MassTransit;

    public class BusModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;

        public BusModule(params System.Reflection.Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Registers all consumers with our container
            builder.RegisterConsumers(_assembliesToScan);

            // Creates our bus from the factory and registers it as a singleton against two interfaces
            builder.Register(c => Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var componentContext = c.Resolve<IComponentContext>();

                var host = sbc.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h =>
                {
                    // Configure your host
                    h.Username("starterkit");
                    h.Password("banana");
                });

                sbc.ReceiveEndpoint(host, "my_messages", e =>
                {
                    // Configure your consumer(s)
                    e.PrefetchCount = 4;
                    e.LoadFrom(componentContext.Resolve<ILifetimeScope>());
                });
            }))
                .As<IBusControl>()
                .As<IBus>();
        }
    }
}
