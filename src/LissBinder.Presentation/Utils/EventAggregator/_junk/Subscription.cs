using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Utils.EventBus
{
    internal class Subscription<TEventBase> : ISubscription where TEventBase : EventBase
    {
        public SubscriptionToken SubscriptionToken { get { return _subscriptionToken; } }

        public Subscription(Action<TEventBase> action, SubscriptionToken token)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (token == null)
                throw new ArgumentNullException("token");

            _action = action;
            _subscriptionToken = token;
        }


        public void Publish(EventBase eventItem)
        {
            if (!(eventItem is TEventBase))
                throw new ArgumentException("Event Item is not the correct type.");

            _action.Invoke(eventItem as TEventBase);
        }

        private readonly Action<TEventBase> _action;
        private readonly SubscriptionToken _subscriptionToken;
    }
}
