namespace StarterKit.Service.Consumer
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class MyConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            Console.Out.WriteLine("Received Message: " + context.Message.Message);
            return Task.FromResult(0);
        }
    }
}
