namespace CZLib.EventBus
{
    using System.Collections.Generic;

    using Microsoft.Practices.ServiceLocation;

    public abstract class EventBusBase : IEventBus
    {
        public void TriggerEvent<T>(T evt) where T : IEventSource
        {
            var handlers = this.FindHandlers<T>();
            this.RunHandlers(handlers, evt);
        }

        protected abstract IEnumerable<IEventHandler<T>> FindHandlers<T>() where T : IEventSource;

        protected virtual void RunHandlers<T>(IEnumerable<IEventHandler<T>> handlers, T evt) where T : IEventSource
        {
            foreach (var handler in handlers)
            {
                handler.HandlerEvent(evt);
            }
        }

    }

    public class EventBus : EventBusBase
    {
        protected override IEnumerable<IEventHandler<T>> FindHandlers<T>()
        {
            return ServiceLocator.Current.GetAllInstances<IEventHandler<T>>();
        }
    }
}