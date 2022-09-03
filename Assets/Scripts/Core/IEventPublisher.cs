namespace MergeMechanic.Core
{
    public interface IEventPublisher<in TInput> where TInput : IEvent
    {
        EventListenerStatus PublishEvent(TInput input);
    }
}
