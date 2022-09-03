using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeMechanic.Core
{
    public class EventPublisher<TInput> : IEventPublisher<TInput> where TInput : IEvent
    {
        private readonly IReadOnlyList<IEventListener<TInput>> _eventListeners;

        public EventPublisher(IEnumerable<IEventListener<TInput>> eventListeners)
        {
            _eventListeners = eventListeners.OrderBy(listener => listener.GetType().Name).ToList();
        }

        public EventListenerStatus PublishEvent(TInput input)
        {
            var eventId = Guid.NewGuid().ToString();

            var failedEventListeners = new List<IEventListener<TInput>>();

            foreach (var listener in _eventListeners)
            {
                var status = listener.OnEvent(input);

                if (status == EventListenerStatus.Fail)
                {
                    failedEventListeners.Add(listener);
                }
            }

            return failedEventListeners.Any()
                ? EventListenerStatus.Fail
                : EventListenerStatus.Success;
        }
    }

    public enum EventListenerStatus
    {
        DoNotUse,
        Success,
        Fail,
    }

    public interface IEvent { }

    public class EventHardFailException : Exception
    {
        public EventHardFailException(string eventType, string listenerType, string eventId) : base("An event returned an unexpected hard fail")
        {
            EventType = eventType;
            ListenerType = listenerType;
            EventId = eventId;
        }

        public string EventType { get; }

        public string ListenerType { get; }

        public string EventId { get; }
    }
}
