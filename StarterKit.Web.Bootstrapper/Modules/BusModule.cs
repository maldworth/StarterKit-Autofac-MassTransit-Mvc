namespace StarterKit.Web.Bootstrapper.Modules
{
    using System;
    using Autofac;
    using MassTransit;
    using System.Configuration;

    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Creates our bus from the factory and registers it as a singleton against two interfaces
            builder.Register(c => Bus.Factory.CreateUsingRabbitMq(sbc => sbc.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h =>
            {
                h.Username("starterkit");
                h.Password("banana");
            })))
                .As<IBusControl>()
                .As<IBus>()
                .SingleInstance();
        }
    }
}
