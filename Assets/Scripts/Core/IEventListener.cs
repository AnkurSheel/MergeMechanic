namespace MergeMechanic.Core
{
    public interface IEventListener<in TInput> where TInput : IEvent
    {
        EventListenerStatus OnEvent(TInput input);
    }
}
