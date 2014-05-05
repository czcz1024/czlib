namespace CZLib.EventBus
{
    public interface IEventHandler<in T>
        where T : IEventSource
    {
        void HandlerEvent(T source);
    }
}