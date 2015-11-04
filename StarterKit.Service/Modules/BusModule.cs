namespace StarterKit.Service.Modules
{
    using System;
    using System.Configuration;
    using Autofac;
    using MassTransit;
    using MassTransit.Log4NetIntegration;

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
                    h.Username(ConfigurationManager.AppSettings["RabbitMQUsername"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMQPassword"]);
                });

                sbc.ReceiveEndpoint(host, ConfigurationManager.AppSettings["MyMessageQueueName"], e =>
                {
                    // Configure your consumer(s)
                    e.PrefetchCount = 4;
                    e.LoadFrom(componentContext.Resolve<ILifetimeScope>());
                });
            }))
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();
        }
    }
}
