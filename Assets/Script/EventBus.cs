﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Script
{
    public class EventBus : IEventBus
    {
            private readonly IEventBusConfiguration _eventBusConfiguration;
            public EventBus(IEventBusConfiguration configuration = null)
            {
                _eventBusConfiguration = configuration ?? EventBusConfiguration.Default;
                _subscriptions = new Dictionary<Type, List<ISubscription>>();
            }

            /// <summary>
            /// Subscribes to the specified event type with the specified action
            /// </summary>
            public SubscriptionToken Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventItem
        {
                if (action == null)
                    throw new ArgumentNullException(nameof(action));

                lock (SubscriptionsLock)
                {
                    if (!_subscriptions.ContainsKey(typeof(TEventBase)))
                        _subscriptions.Add(typeof(TEventBase), new List<ISubscription>());

                    var token = new SubscriptionToken(typeof(TEventBase));
                    _subscriptions[typeof(TEventBase)].Add(new Subscription<TEventBase>(action, token));
                    return token;
                }
            }

            /// <summary>
            /// Unsubscribe from the Event type related to the specified <see cref="SubscriptionToken"/>
            /// </summary>
            public void Unsubscribe(SubscriptionToken token)
            {
                if (token == null)
                    throw new ArgumentNullException(nameof(token));

                lock (SubscriptionsLock)
                {
                    if (_subscriptions.ContainsKey(token.TEventType))
                    {
                        var allSubscriptions = _subscriptions[token.TEventType];
                        var subscriptionToRemove = allSubscriptions.FirstOrDefault(x => x.SubscriptionToken.Token == token.Token);
                        if (subscriptionToRemove != null)
                            _subscriptions[token.TEventType].Remove(subscriptionToRemove);
                    }
                }
            }

            /// <summary>
            /// Publishes the specified event to any subscribers for the event type
            /// </summary>
            public void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventItem
        {
                if (eventItem == null)
                    throw new ArgumentNullException(nameof(eventItem));

                var allSubscriptions = new List<ISubscription>();
                lock (SubscriptionsLock)
                {
                    if (_subscriptions.ContainsKey(typeof(TEventBase)))
                        allSubscriptions = _subscriptions[typeof(TEventBase)].ToList();
                }

                for (var index = 0; index < allSubscriptions.Count; index++)
                {
                    var subscription = allSubscriptions[index];
                    try
                    {
                        subscription.Publish(eventItem);
                    }
                    catch (Exception)
                    {
                        if (_eventBusConfiguration.ThrowSubscriberException)
                            throw;
                    }
                }
            }

            /// <summary>
            /// Publishes the specified event to any subscribers for the <see cref="TEventBase"/> event type asychronously
            /// </summary>
            /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
            /// <typeparam name="TEventBase">The type of event</typeparam>
            /// <param name="eventItem">Event to publish</param>
            public void PublishAsync<TEventBase>(TEventBase eventItem) where TEventBase : EventItem
        {
                PublishAsyncInternal(eventItem, null);
            }

            /// <summary>
            /// Publishes the specified event to any subscribers for the <see cref="TEventBase"/> event type asychronously
            /// </summary>
            /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
            /// <typeparam name="TEventBase">The type of event</typeparam>
            /// <param name="eventItem">Event to publish</param>
            /// <param name="callback"><see cref="AsyncCallback"/> that is called on completion</param>
            public void PublishAsync<TEventBase>(TEventBase eventItem, AsyncCallback callback) where TEventBase : EventItem
        {
                PublishAsyncInternal(eventItem, callback);
            }

            #region PRIVATE METHODS
            private void PublishAsyncInternal<TEventBase>(TEventBase eventItem, AsyncCallback callback) where TEventBase : EventItem
        {
                Task<bool> publishTask = new Task<bool>(() =>
                {
                    Publish(eventItem);
                    return true;
                });
                publishTask.Start();
                if (callback == null)
                    return;

                var tcs = new TaskCompletionSource<bool>();
                publishTask.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        tcs.TrySetException(t.Exception.InnerExceptions);
                    else if (t.IsCanceled)
                        tcs.TrySetCanceled();
                    else
                        tcs.TrySetResult(t.Result);
                    callback?.Invoke(tcs.Task);
                }, TaskScheduler.Default);
            }

            #endregion

            private readonly Dictionary<Type, List<ISubscription>> _subscriptions;
            private static readonly object SubscriptionsLock = new object();

    }
}
