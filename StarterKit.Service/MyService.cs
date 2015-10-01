namespace StarterKit.Service
{
    using Autofac;
    using MassTransit;
    using Topshelf.Logging;

    public class MyService
    {
        readonly LogWriter _log = HostLogger.Get<MyService>();
        private readonly IComponentContext _componentContext;

        IBusControl _busControl;
        BusHandle _busHandle;

        public MyService(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public bool Start()
        {
            _log.Info("Creating bus...");

            _busControl = _componentContext.Resolve<IBusControl>();

            _log.Info("Starting bus...");

            _busHandle = _busControl.Start();

            return true;
        }

        public bool Stop()
        {
            _log.Info("Stopping bus...");

            if (_busHandle != null)
                _busHandle.Stop();

            return true;
        }
    }
}