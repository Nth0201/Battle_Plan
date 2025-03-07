using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Script
{
    internal class Subscription<TEventBase> : ISubscription where TEventBase : EventItem
    {
        public SubscriptionToken SubscriptionToken { get; }

        public Subscription(Action<TEventBase> action, SubscriptionToken token)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            SubscriptionToken = token ?? throw new ArgumentNullException(nameof(token));
        }

        public void Publish(EventItem eventItem)
        {
            if (!(eventItem is TEventBase))
                throw new ArgumentException("Event Item is not the correct type.");

            _action.Invoke(eventItem as TEventBase);
        }

        private readonly Action<TEventBase> _action;
    }
}
