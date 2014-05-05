namespace CZLib.EventBus
{
    public interface IEventBus
    {
        void TriggerEvent<T>(T evt) where T : IEventSource;
    }
}