using System;

namespace Assets.Script
{
    public interface IEventBus
    {
        public void Publish<TEvent>(TEvent eventItem) where TEvent : EventItem;
        public void PublishAsync<TEvent>(TEvent eventItem) where TEvent : EventItem;
        public void PublishAsync<TEvent>(TEvent eventItem, AsyncCallback callback) where TEvent : EventItem;
        public SubscriptionToken Subscribe<TEvent>(Action<TEvent> action) where TEvent : EventItem;
        public void Unsubscribe(SubscriptionToken Token);
    }
}
